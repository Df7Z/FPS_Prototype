using ECS_MONO;
using UnityEngine;

namespace Game.Damage
{
    internal sealed class HealTransactionSystem : EcsSystem<HealTransaction>
    {
        protected override void Run(Entity e, HealTransaction c1)
        {
            HandleTransaction(c1);
            
            _world.DestroyEntity(e);
        }

        private void HandleTransaction(HealTransaction transaction)
        {
            var runtime = transaction.Target.Get<DamagedRuntime>();
            var damaged = transaction.Target.Get<Damaged>();
            
            if (runtime.IsDead) return;

            if (transaction.Data.Bonus)
            {
                runtime.Add(transaction.Data.Count);
            }
            else
            {
                var newValue = runtime.Current + transaction.Data.Count;

                if (newValue > damaged.EnduranceDefault) newValue = damaged.EnduranceDefault;
                
                runtime.Set(newValue);
            }
        }
    }
}