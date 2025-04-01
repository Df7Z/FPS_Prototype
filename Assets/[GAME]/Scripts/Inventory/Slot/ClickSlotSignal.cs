using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class ClickSlotSignal : EcsComponent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}