using ECS_MONO;
using Game.Damage;
using Game.Projectile.Shared;
using PoolSystem;
using UnityEngine;

namespace Game.Projectile
{
    internal sealed class RaycastProjectileSystem : EcsSystemMono<RaycastProjectileObject, ProjectileDataRuntime>
    {
        protected override void Run(EntityMono e, RaycastProjectileObject projectile, ProjectileDataRuntime runtimeData)
        {
            Handle(projectile, (RaycastProjectileData) runtimeData.Data);
        }

        private void Handle(RaycastProjectileObject projectile, RaycastProjectileData data)
        {
            var source = projectile.Owner.Get<ProjectileObjectSource>().Source;
            
            Ray ray = new Ray()
            {
                origin = projectile.transform.position,
                direction = projectile.transform.forward
            };
            
            Debug.DrawRay(ray.origin, ray.direction * data.MaxDistance, Color.red, Time.deltaTime);
            
            //Simple ray
            if (Physics.Raycast(ray, out RaycastHit hit, data.MaxDistance))
            {
                if (hit.collider.gameObject.TryGetComponent(out EntityReference reference))
                {
                    if (reference.Entity.TryGet(out Damaged damaged))
                    {
                        data.DamageData.CreateTransaction(damaged, source);
                    }
                }
            }
            
            SystemPool.Despawn(projectile.gameObject);
        }
    }
    
}