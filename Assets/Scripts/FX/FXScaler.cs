using System;
using UnityEngine;

namespace FX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class FXScaler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;

        private float _defaultMultiplier;
        private void OnValidate()
        {
            particles ??= GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            _defaultMultiplier = particles.main.startSizeMultiplier;
        }

        public void SetScale(float scale)
        {
            var main = particles.main;
            main.startSizeMultiplier = scale;
        }

        public void ResetScale()
        {
            SetScale(_defaultMultiplier);
        }
        
    }
}