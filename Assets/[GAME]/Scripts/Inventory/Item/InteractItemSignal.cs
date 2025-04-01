using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class InteractItemSignal : EcsComponent
    {
        public Slot Slot;
        
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}