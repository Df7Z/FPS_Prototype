using ECS_MONO;

namespace Game.AI
{
    internal sealed class AITaskPatrol : AITask
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}