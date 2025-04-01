using ECS_MONO;
using Game.AI;
using Game.AI.Shared;
using Game.Mob.AI;
using UnityEngine;

namespace Game.AI
{
    internal sealed class MeleeAttackExpectationSystem : EcsSystemMono<AITaskMeleeAttack, MeleeAttackRuntime, MeleeAttack, AIProcess>
    {
        protected override void Run(EntityMono e, AITaskMeleeAttack active, MeleeAttackRuntime runtime, MeleeAttack attack, AIProcess process)
        {
            if (runtime.Time > 0f)
            {
                runtime.Time -= Time.deltaTime;
                
                return;
            }
            
            e.Del<AITaskMeleeAttack>();
        }
    }
}