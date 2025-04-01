using ECS_MONO;

namespace Game.AI.Shared
{
    public sealed class AIProcess : EcsComponent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}