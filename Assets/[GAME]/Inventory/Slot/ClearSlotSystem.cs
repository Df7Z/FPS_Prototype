using ECS_MONO;
using PoolSystem;

namespace Game.Inventory
{
    internal sealed class ClearSlotSystem : EcsSystemMono<Slot, ClearSlotSignal>
    {
        protected override void Run(EntityMono e, Slot slot, ClearSlotSignal c2)
        {
            Clear(e, slot);
            
            e.Del<ClearSlotSignal>();
        }

        private void Clear(EntityMono e, Slot slot)
        {
            if (slot.IsEmpty) return;
            
            if (e.TryGet(out HotSlot hotSlot))
            {
                if (hotSlot.HasView)
                {
                    SystemPool.Despawn(hotSlot.HotView.gameObject);
                    
                    hotSlot.SetView(null);
                }
             
                //hotSlot.SetActive(false);
                //hotSlot.Owner.SafeDel<SelectedHot>();
            }
            
            slot.Clear();
        }
    }
}