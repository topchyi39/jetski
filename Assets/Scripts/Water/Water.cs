using UnityEngine;

namespace Water
{
    public class Water : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 1f)] private float amplitude;

        private static float Time;
        private static float Amplitude;

        private void Start()
        {
            Time = 0;
            Amplitude = amplitude;
        }

        private void Update()
        {
            Time += UnityEngine.Time.deltaTime;
        }

        public static float GetWaterLevel(Vector3 position)
        {
            return Mathf.Sin(position.z + Time) * Amplitude;
        }
    }
}