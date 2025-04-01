using ECS_MONO;
using Game.AI.Shared;

namespace Game.Mobs
{
    internal sealed class StartAISystem : EcsSystemMono<StartAISignal, Mob>
    {
        protected override void Run(EntityMono e, StartAISignal c1, Mob mob)
        {
            mob.AI.Owner.SafeAdd<AIProcess>();
            
            e.Del<StartAISignal>();
        }
    }
}