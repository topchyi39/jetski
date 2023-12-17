using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("jetskiSki")] [FormerlySerializedAs("jetSki")] [SerializeField] private JetSkiController jetSkiControllerSki;
        
    private MovementActions _actions;
        
    private float xPosition;
        
    private void OnEnable()
    {
        _actions ??= new MovementActions();
            
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void Update()
    {
        var steering = Mathf.Clamp(_actions.JetSki.Horizontal.ReadValue<float>(), -1f, 1f);
        jetSkiControllerSki.SetTurn(steering);
    }
}