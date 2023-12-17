using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace WorldUtils
{
    [RequireComponent(typeof(OverrideTransform))]
    public class CollideConstraint : MonoBehaviour
    {
        [SerializeField] private OverrideTransform overrideTransform;
        [SerializeField] private float collideTime;
        [SerializeField] private float collideXRotation;
        
        private void OnValidate()
        {
            overrideTransform ??= GetComponent<OverrideTransform>();
        }

        public void Collide()
        {
            StartCoroutine(CollideRoutine());
        }

        private IEnumerator CollideRoutine()
        {
            yield return LerpAngle(0, collideXRotation, collideTime / 2);
            yield return LerpAngle(collideXRotation, 0, collideTime);
        }

        private IEnumerator LerpAngle(float start, float end, float duration)
        {
            var time = 0f;

            while (time < duration)
            {
                var angle = Mathf.Lerp(start, end, time / duration);
                overrideTransform.data.rotation = new Vector3(angle, 0, 0);
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
        }
    }
}