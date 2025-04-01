using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class ClearSlotSignal : EcsComponent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}