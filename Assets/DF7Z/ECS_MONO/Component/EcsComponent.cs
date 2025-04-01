using System;

namespace ECS_MONO
{
    public abstract class EcsComponent : IEcsComponent
    {
        public virtual uint Order => 0;
        public IEntity Owner => _entity;
        ComponentType IEcsComponent.ComponentType => ComponentType.Default;
        
        private IEntity _entity;

        protected void Delete<C>(C component) where C : class, IEcsComponent, new()
        {
            Owner.SilentDel(component);
        }

        void IEcsComponent.RegisterEntity(IEntity entity)
        {
            _entity = entity;
            
            OnRegisterEntity(entity);
        }

        void IEcsComponent.UnregisterEntity(IEntity entity)
        {
            OnUnregisterEntity(entity);
            
            _entity = null;
        }

        void IEcsComponent.OnOwnerEntitySpawnPool(IEntity entity) => OnSpawnPool();
        void IEcsComponent.OnOwnerEntityDespawnPool(IEntity entity) => OnDespawnPool();
        
        protected virtual void OnRegisterEntity(IEntity entity) {}
        protected virtual void OnUnregisterEntity(IEntity entity) {}

        protected virtual void OnSpawnPool() { }
        protected virtual void OnDespawnPool() { }
    }
}