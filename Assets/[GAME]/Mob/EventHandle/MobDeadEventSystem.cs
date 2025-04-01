using ECS_MONO;
using Game.Mobs.Shared;
using PoolSystem;
using UnityEngine;

namespace Game.Mobs.EventHandle
{
    internal class MobDeadEventSystem : EcsSystemMono<MobDeathEvent, Mob>
    {
        protected override void Run(EntityMono e, MobDeathEvent mobEvent, Mob mob)
        {
            e.Del<MobDeathEvent>();
            
            
            SystemPool.Despawn(mob.gameObject);
        }
    }
}