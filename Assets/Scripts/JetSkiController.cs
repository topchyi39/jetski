using System;
using UnityEngine;
using UnityEngine.Events;

public class JetSkiController : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float enginePower;
    [SerializeField] private float horizontalDrag;
    [SerializeField] private float steeringPower = 1f;
    [Space]
    [SerializeField] private UnityEvent onCollision;
    [SerializeField] private UnityEvent onSteeringLeft;
    [SerializeField] private UnityEvent onSteeringRight;
    [SerializeField] private UnityEvent onSteeringReset;

    private float _prevSteering;
    private float _steering;
    private float _currentSteering;

    private Vector3 _currentRotation;
    private Vector3 _currentRotationVelocity;

    private bool _inBorder;

    private Collider _lastColliderObject;
    
    private const float yAngle = 60f;
    private const float zAngle = 30f;
    private const float xAngle = -10f;
        
    private void OnValidate()
    {
        body ??= GetComponent<Rigidbody>();
    }
        
    private void Update()
    {
        _currentSteering = Mathf.SmoothStep(_currentSteering, _steering, Time.deltaTime * steeringPower);
    }

    private void OnCollisionEnter(Collision other)
    { 
        if (_lastColliderObject == other.collider) return;
        
        _lastColliderObject = other.collider;
        onCollision?.Invoke();
    }

    private void FixedUpdate()
    {
        ProcessRotation();
        ProcessForward();
    }

    private float LimitHorizontal(float steering)
    {
        var currentPosition = body.position;
        var nextPosition = currentPosition + body.velocity * Time.deltaTime;
        var side = nextPosition.x > 0 ? 1 : -1;
        var sign = (int)steering != 0 ? (int)steering : (int)_prevSteering;
        
        switch (Mathf.Abs(nextPosition.x))
        {
            case > 4f:
                return -side;
            case > 2 when side == (int)Mathf.Sign(sign):
                _inBorder = true;
                return 0;
            default:
                _inBorder = false;
                return steering;
        }
    }

    private void ProcessForward()
    {
        var force = transform.forward * enginePower;
        force.y *= 1.5f;
        body.AddForce(force, ForceMode.Acceleration);
        var drag = _inBorder ? horizontalDrag * 4 : horizontalDrag;

        if (Mathf.Abs((int)_steering) != 0) return;
        
        var horizontalVelocity =
            Vector3.Scale(Vector3.right, body.velocity) * (drag * Time.fixedDeltaTime);

        body.AddForce(-horizontalVelocity, ForceMode.VelocityChange);
    }

    private void ProcessRotation()
    {
        _currentRotation.y = Mathf.SmoothDampAngle(
            _currentRotation.y,
            yAngle * _currentSteering,
            ref _currentRotationVelocity.y,
            Time.deltaTime);
            
        _currentRotation.z = Mathf.SmoothDampAngle(
            _currentRotation.z,
            zAngle * -_currentSteering,
            ref _currentRotationVelocity.z,
            Time.deltaTime);

        var xMultiplier = 1f - Mathf.Abs(_currentSteering);
        _currentRotation.x = Mathf.SmoothDampAngle(
            _currentRotation.x,
            xAngle * xMultiplier,
            ref _currentRotationVelocity.x,
            Time.deltaTime);

        var targetRotation = Quaternion.Euler(_currentRotation);
        body.MoveRotation(Quaternion.Normalize(targetRotation));
    }
        

    public void SetTurn(float steering)
    {
        _steering = LimitHorizontal(steering);;
            
        if (_steering < 0) onSteeringLeft?.Invoke();
        else if (_steering > 0) onSteeringRight?.Invoke();
        else onSteeringReset?.Invoke();
    }
}