using ECS_MONO;
using Game.Inventory.Shared;

namespace Game.Inventory
{
    public class PlayerResetInventorySystem : EcsSystemMono<PlayerInventory, PlayerInventoryResetSignal> 
    {
        protected override void Run(EntityMono e, PlayerInventory inventory, PlayerInventoryResetSignal signal)
        {
            e.Del<PlayerInventoryResetSignal>();
            
            inventory.Collector.Get<ItemCollector>().Clear();
        }
    }
}