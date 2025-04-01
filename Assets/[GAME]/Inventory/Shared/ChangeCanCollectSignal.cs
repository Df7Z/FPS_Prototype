using ECS_MONO;

namespace Game.Inventory.Shared
{
    public sealed class ChangeCanCollectSignal : EcsComponent
    {
        public bool State;
        
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}