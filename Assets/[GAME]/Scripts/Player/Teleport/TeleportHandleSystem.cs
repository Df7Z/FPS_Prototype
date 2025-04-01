using ECS_MONO;
using Game.Player.Look;
using Game.Player.Move;
using UnityEngine;

namespace Game.Player.Teleport
{
    internal sealed class TeleportHandleSystem : EcsSystemMono<PlayerMovementView, PlayerMovementRuntime, TeleportSignal>
    {
        protected override void Run(EntityMono e, PlayerMovementView view, PlayerMovementRuntime runtime, TeleportSignal signal)
        {
            runtime.Velocity = Vector3.zero;
            
            view.CharacterController.enabled = false;
            
            view.CharacterController. gameObject.transform.position = signal.Position;
            
            var look = e.Add<PlayerMouseLookChangeSignal>();

            look.Target = signal.Rotation;
            
            view.ViewMouseLook.rotation = signal.Rotation;
            
            view.CharacterController.enabled = true;
            
            e.Del<TeleportSignal>();
        }
    }
}