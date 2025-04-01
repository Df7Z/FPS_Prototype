using ECS_MONO;
using Game.Damage;
using Game.Inventory.Shared;
using Game.Level.Shared;
using Game.Player.Teleport;
using UnityEngine;

namespace Game.Player.Dead
{
    internal sealed class PlayerRespawnSystem : EcsSystemMono<PlayerTag, LevelRestartSignal>
    {
        protected override void Run(EntityMono e, PlayerTag tag, LevelRestartSignal restartSignal)
        {
            e.Add<ChangeCursorSignal>().Target = CursorLockMode.Locked;

            var inventory = GetWorld(WorldId.Inventory).GetFirstWorldComponent<PlayerInventory>();

            inventory.Collector.Add<ChangeCanCollectSignal>().State = true;
            
            inventory.Owner.Add<PlayerInventoryResetSignal>();
            
            //Teleport to spawn
            var spawnPoint = restartSignal.PlayerSpawn;

            TeleportSignal teleport = null;
            
            if (e.Has<TeleportSignal>())
            {
                teleport = e.Get<TeleportSignal>();
            }
            else
            {
                teleport = e.Add<TeleportSignal>();
            }

            teleport.Position = spawnPoint.position;
            teleport.Rotation = spawnPoint.rotation;
            
            //Reset health
            e.Get<Damaged>().ResetToDefault();
            
            //Reset inventory from save
            Debug.Log("Reset inventory save");
            
            e.Del<LevelRestartSignal>();
        }
    }
}