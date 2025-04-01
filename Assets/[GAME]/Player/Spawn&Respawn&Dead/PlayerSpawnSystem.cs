using ECS_MONO;
using Game.Inventory.Shared;
using UnityEngine;

namespace Game.Player.Dead
{
    internal sealed class PlayerSpawnSystem : EcsSystemMono<PlayerTag, PlayerSpawnSignal>
    {
        protected override void Run(EntityMono e, PlayerTag tag, PlayerSpawnSignal signal)
        {
            e.Add<ChangeCursorSignal>().Target = CursorLockMode.Locked;
            
            var inventory = GetWorld(WorldId.Inventory).GetFirstWorldComponent<PlayerInventory>();
            
            inventory.Collector.Add<ChangeCanCollectSignal>().State = true;
            
            e.Del<PlayerSpawnSignal>();
        }
    }
}