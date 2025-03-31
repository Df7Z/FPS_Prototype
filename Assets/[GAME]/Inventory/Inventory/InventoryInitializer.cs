using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    internal sealed class InventoryInitializer : EcsComponent
    {
        [SerializeField] private SlotView[] _slotViews;
        
        public SlotView[] SlotViews => _slotViews;
    }
}