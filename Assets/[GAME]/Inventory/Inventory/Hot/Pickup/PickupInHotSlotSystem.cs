using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class PickupInHotSlotSystem : EcsSystemMono<Slot, HotSlot, PickupInHotSlotSignal>
    {
        protected override void Run(EntityMono e, Slot slot, HotSlot hotSlot, PickupInHotSlotSignal signal)
        {
            if (!slot.IsEmpty)
            {
                var newSignal = slot.Owner.Add<CreateViewForHotSlotSignal>();

                newSignal.HotInventory = signal.Inventory.Owner.Get<HotInventory>();
            }
            
            e.Del<PickupInHotSlotSignal>();
        }
    }
}