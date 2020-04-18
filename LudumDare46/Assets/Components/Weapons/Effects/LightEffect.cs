using UnityEngine;

namespace Assets.Components.Weapons.Effects
{
    [RequireComponent(typeof(Light))]
    public class LightEffect : WeaponEffectsBase
    {
        public float initialIntensity = 6.0F;
        /// <summary>
        /// How long it takes for the light intensity to fade.
        /// </summary>
        public float dissipationTime = 0.05F;
        private Light _light;

        public override bool IsExecuting { get => _light.intensity > 0; set { } }

        private void Awake ()
        {
            _light = GetComponent<Light>();
            _light.enabled = false;
        }

        public override void Execute (WeaponBase weapon)
        {
            _light.intensity = initialIntensity;

            _light.enabled = true;
        }

        // TODO Optimize. No need to have a update look. Use coroutine instead
        private void Update ()
        {
            _light.intensity -= (Time.deltaTime / dissipationTime);

            if (_light.intensity <= 0)
            {
                _light.enabled = false;
            }
        }
    }
}