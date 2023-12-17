using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Buoyancies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Buoyancy : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        [Space]
        [SerializeField] private float force = 20f;
        [SerializeField] private UnityEvent underwater;
        [SerializeField] private UnityEvent upperWater;

        private bool _isUnderWater;
        
        private void OnValidate()
        {
            body ??= GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var position = body.position;

            if (position.y < 0)
            {
                var verticalForce = Mathf.Abs(Physics.gravity.y) * force;
                body.AddForce(new Vector3(0,verticalForce, 0), ForceMode.Impulse);
                _isUnderWater = true;
                underwater?.Invoke();
            }
            else if (_isUnderWater)
            {
                _isUnderWater = false;
                upperWater?.Invoke();
            }
        }
        
    }
}