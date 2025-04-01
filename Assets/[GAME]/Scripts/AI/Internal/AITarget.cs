using ECS_MONO;
using UnityEngine;

namespace Game.AI
{
    internal class AITarget : EcsComponentMono
    {
        private EntityMono _target;

        public EntityMono Target => _target;
        public bool HasTarget => _target != null;

        public void Set(EntityMono value) => _target = value;

        public override void OnDespawnPool(IEntity entity)
        {
            Set(null);
        }
    }
}