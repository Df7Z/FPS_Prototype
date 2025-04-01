using ECS_MONO;
using Game.Mobs.EventHandle;

namespace Game.Mobs
{
    public sealed class MobWorld : EcsWorldMono
    {
        public override WorldId ID => WorldId.Mob;
        protected override void InitSystems()
        {
            CreateUpdateSystem<StartAISystem>();
            CreateUpdateSystem<StopAISystem>();
            
            
            CreateUpdateSystem<MobDeadEventSystem>();
        }
    }
}