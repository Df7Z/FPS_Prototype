using ECS_MONO;

namespace Game.AI.Shared
{
    public sealed class AIProcess : EcsComponent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}