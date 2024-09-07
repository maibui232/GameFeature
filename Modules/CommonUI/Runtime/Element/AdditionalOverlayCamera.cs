namespace Modules.CommonUI.Runtime.Element
{
    using UnityEngine;
    using UnityEngine.Rendering.Universal;
    using UnityEngine.SceneManagement;

    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(UniversalAdditionalCameraData))]
    public class AdditionalOverlayCamera : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Camera                        overlayCamera;
        [SerializeField, HideInInspector] private UniversalAdditionalCameraData additionalCameraData;

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.overlayCamera        ??= this.GetComponent<Camera>();
            this.additionalCameraData ??= this.GetComponent<UniversalAdditionalCameraData>();

            if (this.additionalCameraData == null) return;
            this.additionalCameraData.renderType = CameraRenderType.Overlay;
        }
#endif

        private void Awake()
        {
            this.AssignOverlayCamera();
            SceneManager.activeSceneChanged += OnActiveSceneChange;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChange;
        }

        private void OnActiveSceneChange(Scene arg0, Scene arg1)
        {
            this.AssignOverlayCamera();
        }

        private void AssignOverlayCamera()
        {
            var mainCam = Camera.main;
            if (mainCam != null)
            {
                var cameraData = mainCam.GetComponent<UniversalAdditionalCameraData>();
                if (cameraData != null)
                {
                    if (cameraData.cameraStack.Contains(this.overlayCamera)) return;
                    cameraData.cameraStack.Add(this.overlayCamera);
                }
            }
        }
    }
}