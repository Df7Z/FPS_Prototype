using System;

namespace ECS_MONO
{
    public interface IEcsComponent
    {
        public virtual uint Order => 0; //order initialize when entity instantiate on scene 
        public IEntity Owner { get; }
        public ComponentType ComponentType { get; }
        public void RegisterEntity(IEntity entity);
        public void UnregisterEntity(IEntity entity);
       // bool OnDespawnPoolDel { get; } //Delete when despawn to pool
        public virtual void OnSpawnPool() {}
        public virtual void OnDespawnPool() {}
    }
}