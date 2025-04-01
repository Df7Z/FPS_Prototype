using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class ClearSlotSignal : EcsComponent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}