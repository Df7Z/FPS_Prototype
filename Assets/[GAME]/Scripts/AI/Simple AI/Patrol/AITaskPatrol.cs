using ECS_MONO;

namespace Game.AI
{
    internal sealed class AITaskPatrol : AITask
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}