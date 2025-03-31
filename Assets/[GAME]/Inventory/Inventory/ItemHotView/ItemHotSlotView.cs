using System;
using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class ItemHotSlotView : EcsComponentMono
    {
        private HotSlot _ownerHotSlot;
        private Slot _ownerSlot;
        
        
        public HotSlot OwnerHotSlot => _ownerHotSlot;
        public Slot OwnerSlot => _ownerSlot;

        public void SetOwnerHotSlot(HotSlot value) => _ownerHotSlot = value;
        public void SetOwnerSlot(Slot value) => _ownerSlot = value;
    }
}