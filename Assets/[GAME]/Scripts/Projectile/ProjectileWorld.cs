using ECS_MONO;
using Game.Projectile.Shared;

namespace Game.Projectile
{
    public sealed class ProjectileWorld : EcsWorldMono
    {
        public override WorldId ID => WorldId.Projectile;
        protected override void InitSystems()
        {
            CreateUpdateSystem<RaycastProjectileSystem>();
        }
    }
}