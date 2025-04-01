using ECS_MONO;

namespace Game.Inventory.Shared
{
    public sealed class PlayerInventoryResetSignal : EcsComponent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}