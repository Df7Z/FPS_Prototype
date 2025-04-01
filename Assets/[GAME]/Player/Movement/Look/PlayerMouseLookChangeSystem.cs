using ECS_MONO;
using UnityEngine;

namespace Game.Player.Look
{
    internal sealed class PlayerMouseLookChangeSystem : EcsSystemMono<PlayerMouseLookView, PlayerMouseLookRuntime, PlayerMouseLookChangeSignal>
    {
        protected override void Run(EntityMono e, PlayerMouseLookView c1, PlayerMouseLookRuntime c2, PlayerMouseLookChangeSignal c3)
        {
            LookRotation(c1, c2, c3);
            
            e.Del<PlayerMouseLookChangeSignal>();
        }
        
        private void LookRotation(PlayerMouseLookView view, PlayerMouseLookRuntime runtime, PlayerMouseLookChangeSignal signal)
        {
            runtime.YRotation = signal.Target.eulerAngles.y;
            runtime.XRotation = signal.Target.eulerAngles.x;
        
            view.View.rotation = Quaternion.Euler(signal.Target.eulerAngles.x, signal.Target.eulerAngles.y, 0);;
            view.Player.rotation = Quaternion.Euler(0, signal.Target.eulerAngles.y, 0);
        }
    }
}