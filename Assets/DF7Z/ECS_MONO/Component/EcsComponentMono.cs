
using UnityEngine;

namespace ECS_MONO
{
   
    public abstract class EcsComponentMono : MonoBehaviour, IEcsComponent
    {
        
        public virtual uint Order => 0;
        
        public IEntity Owner => _owner;
        public ComponentType ComponentType => ComponentType.Mono;
        
        private IEntity _owner;
        
        public void RegisterEntity(IEntity entity)
        {
            _owner = entity;
            
            OnRegisterEntity(entity);
        }
        
        public void UnregisterEntity(IEntity entity)
        {
            OnUnregisterEntity(entity);
            
            _owner = null;
        }
        
        protected virtual void OnRegisterEntity(IEntity entity) {}
        protected virtual void OnUnregisterEntity(IEntity entity) {}
        public virtual void OnSpawnPool(IEntity entity) {}
        public virtual void OnDespawnPool(IEntity entity) {}
    }
}