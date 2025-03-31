
using UnityEngine;

namespace ECS_MONO
{
   
    public abstract class EcsComponentMono : MonoBehaviour, IEcsComponent
    {
        public virtual bool OnDespawnPoolDel => false;
        public virtual uint Order => 0;
        
        public IEntity Owner => _entity;
        public ComponentType ComponentType => ComponentType.Mono;
        
        private IEntity _entity;
        
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
        public virtual void OnSpawnPool(IEntity entity) {}
        public virtual void OnDespawnPool(IEntity entity) {}
    }
}