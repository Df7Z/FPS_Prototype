using ECS_MONO;
using Game.AI.Shared;

namespace Game.AI
{
    internal sealed class TaskPrioritySystem : EcsSystemMono<AISource, AIProcess>
    {
        protected override void Run(EntityMono e, AISource source, AIProcess process)
        {
            if (e.Has<AITaskMeleeAttack>())
            {
                e.SafeDel<AITaskChase>();
                e.SafeDel<AITaskPatrol>();
                
                return;
            }
            
            if (e.Has<AITaskChase>())
            {
                e.SafeDel<AITaskPatrol>();
                
                return;
            }
            
            if (e.Has<AITaskPatrol>())
            {
                
            }
        }
    }
}