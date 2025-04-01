using ECS_MONO;

namespace Game.Inventory.Shared
{
    public sealed class ChangeCanCollectSignal : EcsComponent
    {
        public bool State;
        
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}