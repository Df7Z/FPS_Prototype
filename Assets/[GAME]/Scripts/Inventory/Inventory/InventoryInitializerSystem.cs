using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class InventoryInitializerSystem : EcsSystemMono<InventoryInitializer, Inventory>
    {
        protected override void Run(EntityMono e, InventoryInitializer initializer, Inventory inventory)
        {
            Slot[] slots = new Slot[initializer.SlotViews.Length];

            for (int i = 0; i < initializer.SlotViews.Length; i++)
            {
                slots[i] = initializer.SlotViews[i].Owner.Get<Slot>();
            }
            
            inventory.SetSlots(slots);
            
            e.Del<InventoryInitializer>();
        }
    }
}