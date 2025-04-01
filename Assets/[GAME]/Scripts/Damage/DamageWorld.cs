using ECS_MONO;

namespace Game.Damage
{
    public class DamageWorld : EcsWorld
    {
        public override WorldId ID => WorldId.Damage;
        protected override void InitSystems()
        {
            CreateUpdateSystem<HealTransactionSystem>();
            CreateUpdateSystem<DamageTransactionSystem>();
        }
    }
}