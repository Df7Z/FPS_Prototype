using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class WeaponShootBlock : EcsComponent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}