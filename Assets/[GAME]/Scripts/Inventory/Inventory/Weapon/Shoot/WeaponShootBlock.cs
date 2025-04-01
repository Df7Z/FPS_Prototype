using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class WeaponShootBlock : EcsComponent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}