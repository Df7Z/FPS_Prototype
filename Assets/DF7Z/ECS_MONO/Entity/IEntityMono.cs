namespace ECS_MONO
{
    public interface IEntityMono : IEntity
    {
        public C AddMono<C>() where C : EcsComponentMono;
    }
}