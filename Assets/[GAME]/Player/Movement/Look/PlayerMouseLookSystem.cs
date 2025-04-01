using ECS_MONO;
using UnityEngine;

namespace Game.Player.Look
{
    internal sealed class PlayerMouseLookSystemMono : EcsSystemMono<PlayerMouseLookView, PlayerMouseLookRuntime, PlayerInput>
    {
        protected override void Run(EntityMono e, PlayerMouseLookView c1, PlayerMouseLookRuntime c2, PlayerInput c3)
        {
            LookRotation(c1, c2, c3);
        }
        
        private void LookRotation(PlayerMouseLookView view, PlayerMouseLookRuntime runtime, PlayerInput input)
        {
            runtime.YRotation += input.Axis.x * runtime.Sens;
            runtime.XRotation -= input.Axis.y * runtime.Sens;
            
            runtime.XRotation = Mathf.Clamp( runtime.XRotation,  -90f, 90f);

            view.View.rotation = Quaternion.Euler(Vector3.up * runtime.YRotation) * Quaternion.Euler(Vector3.right * runtime.XRotation);

            view.Player.Rotate(Vector3.up, input.Axis.x * runtime.Sens);
        }
    }
}
