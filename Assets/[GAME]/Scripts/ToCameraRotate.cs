using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Game
{
    public class ToCameraRotate : MonoBehaviour
    {
        [SerializeField] private bool ExcludeZ;
        [SerializeField] private bool _fix180Rot;
        
        private static Quaternion __fix180;
        private Vector3 __temp;
        
        private void Awake()
        {
            __fix180 = Quaternion.Euler(new Vector3(0, 180, 0));
            
            if (ExcludeZ) {
                if (_fix180Rot)
                {
                    RenderPipelineManager.beginCameraRendering += OnEndCameraRenderingExcludeZFix;
                }
                else
                {
                    RenderPipelineManager.beginCameraRendering += OnEndCameraRenderingExcludeZ;
                }

                __temp.y = 0;
            }
            else {
                if (_fix180Rot)
                {
                    RenderPipelineManager.beginCameraRendering += OnEndCameraRenderingFix;
                }
                else
                {
                    RenderPipelineManager.beginCameraRendering += OnEndCameraRendering;
                }
            }
        }

        private void OnEndCameraRenderingExcludeZ(ScriptableRenderContext context, Camera cam) {
            __temp.x = cam.transform.position.x;
            __temp.y = transform.position.y;
            __temp.z = cam.transform.position.z;
            
            transform.LookAt(__temp, Vector3.up); //если поставить по y  будет летать за камерой
        }
        
        private void OnEndCameraRenderingExcludeZFix(ScriptableRenderContext context, Camera cam) {
            __temp.x = cam.transform.position.x;
            __temp.y = transform.position.y;
            __temp.z = cam.transform.position.z;
            
            transform.LookAt(__temp, Vector3.up); //если поставить по y  будет летать за камерой

            transform.rotation *= __fix180;
        }
        
        private void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
        {
            transform.LookAt(cam.transform.position); //если поставить по y  будет летать за камерой
        }

        private void OnEndCameraRenderingFix(ScriptableRenderContext context, Camera cam)
        {
            transform.LookAt(cam.transform.position); //если поставить по y  будет летать за камерой
            
            transform.rotation *= __fix180;
        }
        
        private void OnDestroy()
        {
            if (ExcludeZ) {
                if (_fix180Rot)
                {
                    RenderPipelineManager.beginCameraRendering -= OnEndCameraRenderingExcludeZFix;
                }
                else
                {
                    RenderPipelineManager.beginCameraRendering -= OnEndCameraRenderingExcludeZ;
                }
            }
            else {
                if (_fix180Rot)
                {
                    RenderPipelineManager.beginCameraRendering -= OnEndCameraRenderingFix;
                }
                else
                {
                    RenderPipelineManager.beginCameraRendering -= OnEndCameraRendering;
                }
            }
           
        }
    }

}