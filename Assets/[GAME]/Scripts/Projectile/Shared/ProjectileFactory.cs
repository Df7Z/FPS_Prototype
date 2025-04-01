using ECS_MONO;
using PoolSystem;
using UnityEngine;

namespace Game.Projectile.Shared
{
    public static class ProjectileFactory
    {
        public static IEntity CreateProjectile(this ProjectileData data, IEntity source, Transform spawn)
        {
            return CreateProjectile(data, source, spawn.position, spawn.rotation);
        }
        
        public static IEntity CreateProjectile(this ProjectileData data, IEntity source, Vector3 position, Quaternion rotation)
        {
            var entity = SystemPool.Spawn(data.Prefab, position, rotation);

            if (entity.TryGet(out ProjectileObjectSource component))
            {
                component.Source = source;
            }
            else
            {
                entity.Add<ProjectileObjectSource>().Source = source;
            }

            entity.Add<ProjectileDataRuntime>().Data = data;
            
            return entity;
        }
    }
}