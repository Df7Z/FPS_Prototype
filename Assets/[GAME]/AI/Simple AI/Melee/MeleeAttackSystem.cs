using ECS_MONO;
using Game.AI.Shared;
using Game.Damage;
using Game.Mobs.Shared;

namespace Game.Mob.AI
{
    internal sealed class MeleeAttackSystem : EcsSystemMono<MeleeAttackSignal, MeleeAttack, AITarget, AISource, AIProcess>
    {
        protected override void Run(EntityMono e, MeleeAttackSignal signal, MeleeAttack attack, AITarget target, AISource source, AIProcess process)
        {
            e.Del<MeleeAttackSignal>();
            
            var runtime = e.SafeAdd<MeleeAttackRuntime>();

            runtime.Time = attack.Time;
            
            //attack
            attack.Damage.CreateTransaction(target.Target.Get<Damaged>(), source.Mob);

            source.Mob.SafeAdd<MobMeleeAttackEvent>(); //Send event for view -> for mob
            
            e.SafeAdd<AITaskProcess>();
        }
        
        
    }
}