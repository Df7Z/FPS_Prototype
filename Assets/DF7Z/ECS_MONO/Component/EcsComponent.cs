using System;

namespace ECS_MONO
{
    public abstract class EcsComponent : IEcsComponent
    {
        public virtual uint Order => 0;
        public IEntity Owner => _entity;
        public ComponentType ComponentType => ComponentType.Default;
        
        private IEntity _entity;

        protected void Delete<C>(C component) where C : class, IEcsComponent, new()
        {
            Owner.SilentDel(component);
        }
        
        public void RegisterEntity(IEntity entity)
        {
            _entity = entity;
            
            OnRegisterEntity(entity);
        }
        
        public void UnregisterEntity(IEntity entity)
        {
            OnUnregisterEntity(entity);
            
            _entity = null;
        }
        
        protected virtual void OnRegisterEntity(IEntity entity) {}
        protected virtual void OnUnregisterEntity(IEntity entity) {}

        public virtual void OnSpawnPool() { }
        public virtual void OnDespawnPool() { }
    }
}