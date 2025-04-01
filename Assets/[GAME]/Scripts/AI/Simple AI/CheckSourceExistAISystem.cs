using ECS_MONO;
using Game.AI.Shared;

namespace Game.Mob.AI
{
    internal sealed class CheckSourceExistAISystem : EcsSystemMono<AISource, AIProcess>
    {
        protected override void Run(EntityMono e, AISource source, AIProcess process)
        {
            if (source.Mob.InPool)
            {
                e.Del<AIProcess>();
            }
        }
    }
}