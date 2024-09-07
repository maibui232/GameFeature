namespace Modules.GameFeel.Runtime.VfxAttractor
{
    using System;
    using System.Collections.Generic;
    using Coffee.UIExtensions;
    using Cysharp.Threading.Tasks;
    using GameCore.Services.ObjectPool;
    using UnityEngine;
    using VContainer;

    public interface IVfxAttractorService
    {
        void SpawnAttractor
        (
            string                       attractorAddressableId,
            Vector3                      screenPosition,
            Transform                    attractor,
            float                        delayRecycle       = 2f,
            float                        attractorRadius    = 1f,
            float                        attractorDelayRate = 0.1f,
            float                        attractorSpeed     = 1f,
            UIParticleAttractor.Movement movement           = UIParticleAttractor.Movement.Sphere
        );

        void RecycleAttractor(VfxAttractorView attractorView);
    }

    [RequireComponent(typeof(Canvas))]
    public class VfxAttractorService : MonoBehaviour, IVfxAttractorService
    {
#region Fields

        [SerializeField] private Transform           overlayTransform;
        [SerializeField] private UIParticleAttractor attractorPrefab;

#endregion

        private readonly Dictionary<VfxAttractorView, UIParticleAttractor> attractorViewToAttractor = new();

#region Inject

        private IObjectPoolService objectPoolService;

#endregion

        [Inject]
        private void Init
        (
            IObjectPoolService objectPoolService
        )
        {
            this.objectPoolService = objectPoolService;
        }

        public async void SpawnAttractor
        (
            string                       attractorAddressableId,
            Vector3                      screenPosition,
            Transform                    attractor,
            float                        delayRecycle       = 2f,
            float                        attractorRadius    = 1f,
            float                        attractorDelayRate = 0.1f,
            float                        attractorSpeed     = 1f,
            UIParticleAttractor.Movement movement           = UIParticleAttractor.Movement.Sphere
        )
        {
            var vfxInstance = await this.objectPoolService.Spawn(attractorAddressableId, this.overlayTransform);
            vfxInstance.transform.position = screenPosition;

            var attractorInstance = this.objectPoolService.Spawn(this.attractorPrefab, this.transform);
            attractorInstance.gameObject.SetActive(true);
            attractorInstance.transform.position = attractor.position;

            attractorInstance.destinationRadius = attractorRadius;
            attractorInstance.delay             = attractorDelayRate;
            attractorInstance.maxSpeed          = attractorSpeed;
            attractorInstance.movement          = movement;

            var attractorView = vfxInstance.GetComponent<VfxAttractorView>();
            attractorInstance.AddParticleSystem(attractorView.AttractorParticleSystem);
            this.attractorViewToAttractor.TryAdd(attractorView, attractorInstance);

            await UniTask.Delay(TimeSpan.FromSeconds(delayRecycle + attractorDelayRate), true);
            this.RecycleAttractor(attractorView);
        }

        public void RecycleAttractor(VfxAttractorView attractorView)
        {
            if (this.attractorViewToAttractor.TryGetValue(attractorView, out var attractor))
            {
                attractor.RemoveParticleSystem(attractorView.AttractorParticleSystem);
                attractorView.Recycle();
                attractor.Recycle();
            }
        }
    }
}