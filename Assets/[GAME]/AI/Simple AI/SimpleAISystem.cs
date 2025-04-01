using System;
using ECS_MONO;
using Game.AI.Shared;
using Game.Damage;
using Game.Mobs.Shared;
using Game.Player;
using UnityEngine;

namespace Game.Mob.AI
{
    internal sealed class SimpleAISystem : EcsSystemMono<SimpleAI, AISource, AIProcess>
    {
        protected override void Run(EntityMono e, SimpleAI ai, AISource source, AIProcess process)
        {
            //Ai update - delta.time
            
            
            
            if (e.Has<AITaskProcess>())
            {
                //Task process, wait finish
                
                return;
            }

            if (!CheckAISourceAlive(source))
            {
                source.Mob.SafeAdd<MobDeathEvent>();
                
                e.Del<AIProcess>(); //Stop ai logic, mob => dead !
                
                return;
            }
            
            var target = e.Get<AITarget>();
            
            CheckTarget(target);

            SwitchTargetCondition(e, ai, source, process, target);
        }

        private void SwitchTargetCondition(EntityMono e, SimpleAI ai, AISource source, AIProcess process, AITarget target)
        {
            if (target.HasTarget) //проверить радиус по тз
            {
                //Check melee attack
                if (!TryMeleeAttack(e, target, ai, source))
                {
                    //Move to target
                    MoveAgent(e, target);
                }
            }
            else
            {
                //Stay => patrolling
                StopAgent(e);
            }
        }
        
        private bool TryMeleeAttack(EntityMono e, AITarget target, SimpleAI ai, AISource source)
        {
            if (EcsGlobalSetup.IsDebug)
            {
                if (source.Mob.transform == null) throw new Exception();
                if (target.Target.transform == null) throw new Exception();
            }
            
            var distance = Vector3.Distance(source.Mob.transform.position, target.Target.transform.position);

            var attack = e.Get<MeleeAttack>();

            if (distance > attack.MinDistance) return false;

            e.Add<MeleeAttackSignal>();
            
            StopAgent(e);
            
            return true;
        }

        private void StopAgent(EntityMono e)
        {
            e.SafeDel<AgentTask>();
        }
        
        private void MoveAgent(EntityMono e, AITarget target)
        {
            SetMoveTask(!e.Has<AgentTask>() ? e.Add<AgentTask>() : e.Get<AgentTask>());

            void SetMoveTask(AgentTask task)
            {
                task.SetDestination(target.Target.transform.position);
            }
        }

        private bool CheckAISourceAlive(AISource source)
        {
            return !source.Mob.Has<DamagedDead>();
        }

        private void CheckTarget(AITarget target)
        {
            if (!target.HasTarget)
            {
                //try find target
                var player = EcsCore.I.GetWorld<EntityMono>(WorldId.Player).GetAnyEntityWith<PlayerTag>();

                if (player != null)
                {
                    if (!player.Has<DamagedDead>())
                    {
                        target.Set(player);
                    }
                }
            }
            else
            {
                //target alive? 
                if (target.Target.InPool || target.Target.Has<DamagedDead>())
                {
                    target.Set(null);
                    
                    return;
                }
            }
        }
    }
}