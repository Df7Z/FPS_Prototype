using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class InteractSlotSystem : EcsSystemMono<Slot, ClickSlotSignal>
    {
        protected override void Run(EntityMono e, Slot slot, ClickSlotSignal signal)
        {
            e.Del<ClickSlotSignal>();
            
            if (slot.IsEmpty) return;
            
            if (!slot.Item.Owner.Has<InteractableItem>()) return;

            if (slot.Item.Owner.Has<InteractItemSignal>()) return;
            
            var interact = slot.Item.Owner.Add<InteractItemSignal>();

            interact.Slot = slot;
        }
    }
}