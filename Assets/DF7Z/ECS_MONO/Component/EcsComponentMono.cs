
using UnityEngine;

namespace ECS_MONO
{
   
    public abstract class EcsComponentMono : MonoBehaviour, IEcsComponent
    {
        public virtual uint Order => 0;
        public IEntity Owner => _owner;
        ComponentType IEcsComponent.ComponentType => ComponentType.Mono;
        
        private IEntity _owner;

        void IEcsComponent.RegisterEntity(IEntity entity)
        {
            _owner = entity;
            
            OnRegisterEntity(entity);
        }

        void IEcsComponent.UnregisterEntity(IEntity entity)
        {
            OnUnregisterEntity(entity);
            
            _owner = null;
        }

        void IEcsComponent.OnOwnerEntitySpawnPool(IEntity entity)
        {
            OnSpawnPool();
        }

        void IEcsComponent.OnOwnerEntityDespawnPool(IEntity entity)
        {
            OnDespawnPool();
        }

        protected virtual void OnRegisterEntity(IEntity entity) {}
        protected virtual void OnUnregisterEntity(IEntity entity) {}
        protected virtual void OnSpawnPool() {}
        protected virtual void OnDespawnPool() {}
    }
}