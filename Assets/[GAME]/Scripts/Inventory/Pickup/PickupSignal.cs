using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class PickupSignal : EcsComponent
    {
        public ItemCollector ItemCollector;

        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}