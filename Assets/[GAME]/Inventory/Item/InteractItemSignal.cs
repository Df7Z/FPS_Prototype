using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class InteractItemSignal : EcsComponent
    {
        public Slot Slot;
        
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}