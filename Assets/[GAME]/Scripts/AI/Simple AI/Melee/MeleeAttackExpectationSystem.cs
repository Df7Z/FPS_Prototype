using ECS_MONO;
using Game.AI.Shared;
using UnityEngine;

namespace Game.Mob.AI
{
    internal sealed class MeleeAttackExpectationSystem : EcsSystemMono<AITaskProcess, MeleeAttackRuntime, MeleeAttack, AIProcess>
    {
        protected override void Run(EntityMono e, AITaskProcess active, MeleeAttackRuntime runtime, MeleeAttack attack, AIProcess process)
        {
            if (runtime.Time > 0f)
            {
                runtime.Time -= Time.deltaTime;
                
                return;
            }
            
            e.Del<AITaskProcess>();
        }
    }
}