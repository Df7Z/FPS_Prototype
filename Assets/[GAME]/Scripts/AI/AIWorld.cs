using ECS_MONO;
using Game.Mob.AI;

namespace Game.AI
{
    public class AIWorld : EcsWorldMono
    {
        public override WorldId ID => WorldId.AI;
        protected override void InitSystems()
        {
           CreateUpdateSystem<CheckSourceExistAISystem>();
           
           CreateUpdateSystem<SimpleAISystem>();
           
           CreateUpdateSystem<AgentMoveSystem>();
           
           CreateUpdateSystem<MeleeAttackSystem>();
           CreateUpdateSystem<MeleeAttackExpectationSystem>();
           
           CreateUpdateSystem<StartPatrolSystem>();
           CreateUpdateSystem<PatrolSystem>();
           
           
           
           
           
           CreateUpdateSystem<TaskPrioritySystem>();
        }
    }
}