using UnityEngine;

namespace ECS_MONO
{
    public abstract class ComponentAdapter<T> : MonoBehaviour, IComponentAdapter where T : EcsComponent, new()
    {
        [SerializeField] protected T _data = new T();

        public uint Order => _data.Order;

        public void SetComponent(EntityMono entityMono)
        {
            Init(entityMono);
            
            entityMono.AdapterAdd(_data);
        }

        public void SilentDestroy() => Destroy(this);
        
        public virtual void Init(EntityMono entityMono) {}
    }
}