using ECS_MONO;

namespace Game.Damage
{
    public static class DataExtenstion
    {
        public static void CreateTransaction(this DamageData damage, Damaged target, IEntity source) 
        {
            var entity = EcsCore.I.GetWorld<Entity>(WorldId.Damage).CreateEntity();

            var transaction = entity.Add<DamageTransaction>();
            
            transaction.Source = source;
            transaction.Target = target.Owner;
         
            transaction.Data = damage;
        }
        
        public static void CreateTransaction(this HealData heal, Damaged target, IEntity source) 
        {
            var entity = EcsCore.I.GetWorld<Entity>(WorldId.Damage).CreateEntity();

            var transaction = entity.Add<HealTransaction>();
            
            transaction.Source = source;
            transaction.Target = target.Owner;
         
            transaction.Data = heal;
        }
    }
}