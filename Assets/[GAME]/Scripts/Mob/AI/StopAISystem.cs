using ECS_MONO;
using Game.AI.Shared;

namespace Game.Mobs
{
    internal sealed class StopAISystem : EcsSystemMono<StopAISignal, Mob>
    {
        protected override void Run(EntityMono e, StopAISignal c1, Mob mob)
        {
            mob.AI.Owner.SafeDel<AIProcess>();
            
            e.Del<StopAISignal>();
        }
    }
}