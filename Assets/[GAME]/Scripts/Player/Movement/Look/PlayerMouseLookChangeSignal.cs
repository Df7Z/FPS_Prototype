using ECS_MONO;
using UnityEngine;

namespace Game.Player.Look
{
    internal sealed class PlayerMouseLookChangeSignal : EcsComponent
    {
        public Quaternion Target;

        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}