using UnityEngine;

namespace WorldUtils
{
    public class ConstraintTarget : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        
        private Transform _defaultParent;
        private Vector3 _localPosition;
        private void Start()
        {
            _localPosition = transform.localPosition;
            _defaultParent = transform.parent;
            // transform.SetParent(null);
        }

        private void LateUpdate()
        {
            transform.position = _defaultParent.position + _localPosition + offset;
        }
    }
}