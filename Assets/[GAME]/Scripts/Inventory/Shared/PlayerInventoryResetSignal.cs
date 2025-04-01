using ECS_MONO;

namespace Game.Inventory.Shared
{
    public sealed class PlayerInventoryResetSignal : EcsComponent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}