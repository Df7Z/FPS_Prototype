using ECS_MONO;
using UnityEngine;

namespace Game.Level.Shared
{
    public sealed class LevelRestartSignal : EcsComponent
    {
        public LevelRestartParams RestartParams;
        public Transform PlayerSpawn;
        
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}