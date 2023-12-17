using System;
using UnityEngine;

namespace WorldUtils
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 positionScaleVector;

        private void LateUpdate()
        {
            var position = Vector3.Scale(positionScaleVector, target.position);
            transform.position = position;
        }
    }
}