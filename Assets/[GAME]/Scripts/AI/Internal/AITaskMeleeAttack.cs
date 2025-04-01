using ECS_MONO;

namespace Game.AI
{
    internal sealed class AITaskMeleeAttack : AITask //Ai give task - this in process
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}