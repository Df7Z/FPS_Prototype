using UnityEngine;

namespace ECS_MONO
{
    public abstract class EcsWorldMono : EcsWorldAbstract<EntityMono>
    {
        public override EntityMono CreateEntity()
        {
            var go = new GameObject("EntityMono");
            
            var entity = go.AddComponent<EntityMono>();

            entity.RegisterInWorld(this);
            
            return entity;
        }

        public override void DestroyEntity(EntityMono entity)
        {
            entity.Clear();
            
            entity.UnregisterFromWorld(this);
            
            Destroy(entity.gameObject);
        }
    }
}