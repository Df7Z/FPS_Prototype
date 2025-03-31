using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class ClickSlotSignal : EcsComponent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}