using ECS_MONO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Inventory
{
    internal sealed class Inventory : EcsComponentMono
    {
        [SerializeField] private uint _pickupOrder;

        public uint PickupOrder => _pickupOrder;

        private Slot[] _slots;
        
        public Slot[] Slots => _slots;

        public void SetSlots(Slot[] value) => _slots = value;
        
    }
}