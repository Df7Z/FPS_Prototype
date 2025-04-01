namespace ECS_MONO
{
    public abstract class EcsWorld : EcsWorldAbstract<Entity>
    {
        public override Entity CreateEntity()
        {
            var entity = EntityPool.Get();

            RegisterEntity(entity);
           
            return entity;
        }

        public override void DestroyEntity(Entity entity)
        {
            entity.Clear();
            
            UnregisterEntity(entity);
            
            EntityPool.Return(entity);
        }
    }
}