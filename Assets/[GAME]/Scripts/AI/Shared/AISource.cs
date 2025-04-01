using ECS_MONO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.AI.Shared
{
    public sealed class AISource : EcsComponentMono
    {
        [FormerlySerializedAs("_source")] [SerializeField] private EntityMono _mob;

        public EntityMono Mob => _mob;
    }
}