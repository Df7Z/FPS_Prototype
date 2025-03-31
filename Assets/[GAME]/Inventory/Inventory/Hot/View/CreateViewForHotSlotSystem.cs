using ECS_MONO;
using PoolSystem;

namespace Game.Inventory
{
    internal sealed class CreateViewForHotSlotSystem : EcsSystemMono<Slot, HotSlot, CreateViewForHotSlotSignal>
    {
        protected override void Run(EntityMono e, Slot slot, HotSlot hotSlot, CreateViewForHotSlotSignal signal)
        {
            if (slot.Item.Owner.Has<HotItem>())
            {
                var viewPrefab = slot.Item.Owner.Get<HotItem>().PrefabView;

                var view = SystemPool.Spawn(viewPrefab, signal.HotInventory.SpawnHotItemView);

                hotSlot.SetView(view);
                
                view.SetOwnerHotSlot(hotSlot);
                view.SetOwnerSlot(slot);
            }

            e.Del<CreateViewForHotSlotSignal>();
        }
    }
}