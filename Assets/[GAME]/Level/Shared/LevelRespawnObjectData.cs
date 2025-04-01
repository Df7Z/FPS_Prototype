using ECS_MONO;
using UnityEngine;

namespace Game.Level.Shared
{
    [CreateAssetMenu(fileName = "RespawnObject", menuName = "Game/Level/RespawnObject SD")]
    public sealed class LevelRespawnObjectData : ScriptableObject
    {
        [SerializeField] private EntityMono _prefab;
        [SerializeField] private bool _setParentNull;

        public bool SetParentNull => _setParentNull;

        public EntityMono Prefab => _prefab;
    }
}