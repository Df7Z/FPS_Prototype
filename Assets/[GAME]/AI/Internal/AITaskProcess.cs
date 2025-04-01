using ECS_MONO;

namespace Game.Mob.AI
{
    internal sealed class AITaskProcess : EcsComponent //Ai give task - this in process
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}