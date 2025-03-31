using ECS_MONO;

namespace Game.Damage
{
    internal sealed class DamageTransactionSystem : EcsSystem<DamageTransaction>
    {
        protected override void Run(Entity e, DamageTransaction c1)
        {
            HandleTransaction(c1);
            
            _world.DestroyEntity(e);
        }

        private void HandleTransaction(DamageTransaction transaction)
        {
            var runtime = transaction.Target.Get<DamagedRuntime>();
            
            if (runtime.IsDead) return;

            runtime.Take(transaction.Data.Count);
        }
    }
}