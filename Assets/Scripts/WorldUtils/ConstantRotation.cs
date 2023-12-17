using UnityEngine;

namespace WorldUtils
{
    public class ConstantRotation : MonoBehaviour
    {
        private Quaternion _worldRotation;

        private void Start()
        {
            _worldRotation = transform.rotation;
        }

        private void LateUpdate()
        {
            transform.rotation = _worldRotation;
        }
    }
}