namespace ECS_MONO
{
    public interface IEcsSystem<E> where E : class, IEntity
    {
        internal void UpdateSys();
        internal void FixedUpdateSys();
        internal void LateUpdateSys();
        internal void DestructFromWorld(EcsWorldAbstract<E> ecsWorldAbstract);
        internal void InitFromWorld(EcsWorldAbstract<E> ecsWorldAbstract, IEcsCore core);
        internal void InitFromСore();
        internal void TryRegisterEntityComponents(E entity);
        internal void TryUnregisterEntityComponents(E entity);
        internal void SilentUnregisterEntity(E entity);
    }
}