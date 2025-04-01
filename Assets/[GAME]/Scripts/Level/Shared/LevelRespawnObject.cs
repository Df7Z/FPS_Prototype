using ECS_MONO;
using UnityEngine;

namespace Game.Level.Shared
{
    public sealed class LevelRespawnObject : EcsComponentMono
    {
        [SerializeField] private LevelRespawnObjectData _data;
        
        public LevelRespawnObjectData Data => _data;
    }
}