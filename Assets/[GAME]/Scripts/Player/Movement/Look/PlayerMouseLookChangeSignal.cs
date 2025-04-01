using ECS_MONO;
using UnityEngine;

namespace Game.Player.Look
{
    internal sealed class PlayerMouseLookChangeSignal : EcsComponent
    {
        public Quaternion Target;

        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}