using ECS_MONO;

namespace Game.Projectile.Shared
{
    public sealed class ProjectileDataRuntime : EcsComponent
    {
        public ProjectileData Data;
        
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}