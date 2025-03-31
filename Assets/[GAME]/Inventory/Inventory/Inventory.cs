using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class Inventory : EcsComponentMono
    {
        private Slot[] _slots;
        
        public Slot[] Slots => _slots;

        public void SetSlots(Slot[] value) => _slots = value;
    }
}