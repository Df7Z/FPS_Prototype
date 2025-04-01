using ECS_MONO;
using UnityEngine;

namespace Game.Player.Teleport
{
    internal class TeleportSignal : EcsComponent
    {
        public Vector3 Position;
        public Quaternion Rotation;

        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}