using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class PickupInHotSlotSignal : EcsComponent
    {
        public Inventory Inventory;
    }
}