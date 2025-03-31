using System;
using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class CountedItemRuntime : EcsComponentMono //Runtime values
    {
        [NonSerialized] public int Current = 1;
        
        public void SetCurrent(int value)
        {
            Current = value;
        }

        public bool TryAdd(int value)
        {
            if (Current + value > Owner.Get<CountedItem>().Max) return false;

            Current += value;
           
            return true;
        }
    }
}