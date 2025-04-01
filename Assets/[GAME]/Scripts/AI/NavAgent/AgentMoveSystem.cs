using ECS_MONO;
using Game.AI.Shared;

namespace Game.Mob.AI
{
    internal sealed class AgentMoveSystem : EcsSystemMono<AgentTask, AIAgent, AIProcess>
    {
        protected override void Run(EntityMono e, AgentTask task, AIAgent aiAgent, AIProcess process)
        {
            aiAgent.Agent.SetDestination(task.Destination);
        }
    }
}