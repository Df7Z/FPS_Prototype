using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    internal class PlayerChangeCursorSystem: EcsSystemMono<PlayerTag, ChangeCursorSignal>
    {
        protected override void Run(EntityMono e, PlayerTag tag, ChangeCursorSignal signal)
        {
            Cursor.lockState = signal.Target;
            
            e.Del<ChangeCursorSignal>();
        }
    }
}