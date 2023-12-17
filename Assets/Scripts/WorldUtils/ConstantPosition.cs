using UnityEngine;

namespace WorldUtils
{
    public class ConstantPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 resetVector;
        
        private void LateUpdate()
        {
            var position = transform.position;

            position = Vector3.Scale(resetVector, position);

            transform.position = position;
        }
    }
}