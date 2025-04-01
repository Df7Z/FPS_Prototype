using System;

namespace ECS_MONO
{
    public interface IEcsComponent
    {
        public virtual uint Order => 0; //order initialize when entity instantiate on scene 
        public IEntity Owner { get; }
        internal ComponentType ComponentType { get; }
        internal void RegisterEntity(IEntity entity);
        internal void UnregisterEntity(IEntity entity);
        internal void OnOwnerEntitySpawnPool(IEntity entity); //Awake || spawn from pool
        internal void OnOwnerEntityDespawnPool(IEntity entity); //Return to pool
    }
}