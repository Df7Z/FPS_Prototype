﻿using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class PickupSignal : EcsComponent
    {
        public Item PickupItem;
        public ItemCollector ItemCollector;
    }
}