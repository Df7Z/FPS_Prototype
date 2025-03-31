namespace ECS_MONO
{
    public abstract class EcsWorld : EcsWorldAbstract<Entity>
    {
        public override Entity CreateEntity()
        {
            var entity = EntityPool.Get();

            entity.RegisterInWorld(this);
            
            return entity;
        }

        public override void DestroyEntity(Entity entity)
        {
            entity.Clear();
            
            entity.UnregisterFromWorld(this);
            
            EntityPool.Return(entity);
        }
    }
}