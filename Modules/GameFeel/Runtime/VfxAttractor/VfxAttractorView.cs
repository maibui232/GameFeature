namespace Modules.GameFeel.Runtime.VfxAttractor
{
    using Coffee.UIExtensions;
    using UnityEngine;

    [RequireComponent(typeof(UIParticle))]
    public class VfxAttractorView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem attractorParticleSystem;

        public ParticleSystem AttractorParticleSystem => this.attractorParticleSystem;
    }
}