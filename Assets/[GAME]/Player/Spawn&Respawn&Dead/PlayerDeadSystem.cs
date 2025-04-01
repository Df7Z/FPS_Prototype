using ECS_MONO;
using Game.Damage;
using Game.Inventory.Shared;
using Game.Level.Shared;
using Game.Player.UI;
using UnityEngine;

namespace Game.Player.Dead
{
    internal sealed class PlayerDeadSystem : EcsSystemMono<PlayerScreens, Damaged, DamagedDead>
    {
        protected override void Run(EntityMono e, PlayerScreens screens, Damaged damaged, DamagedDead dead)
        {
            if (screens.DeadScreen.IsActive) return;

            if (e.Has<LevelRestartSignal>()) return;
            
            e.Add<ChangeCursorSignal>().Target = CursorLockMode.None;
            
            var inventory = GetWorld(WorldId.Inventory).GetFirstWorldComponent<PlayerInventory>();
            
            inventory.Collector.Add<ChangeCanCollectSignal>().State = false;
            
            screens.DeadScreen.SetView(true);
        }
    }
}