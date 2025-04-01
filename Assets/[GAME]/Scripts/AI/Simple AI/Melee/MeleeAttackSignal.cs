using ECS_MONO;
using Game.Damage;

namespace Game.AI
{
    internal sealed class MeleeAttackSignal : EcsComponent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}