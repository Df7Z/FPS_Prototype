namespace ECS_MONO
{
    public interface IEcsWorld<T> where T : IEntity
    {
        public T CreateEntity();
        public void DestroyEntity(T entity);
        public void RegisterEntity(T entity);
        public void UnregisterEntity(T entity);
       // public bool ExistRegistryEntity(T entity);
        public T GetAnyEntityWith<C>() where C : class, IEcsComponent;
    }
}