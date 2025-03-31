using UnityEngine;

namespace ECS_MONO.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "Game/Level/SD")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private GameLevel _levelPrefab;

        public GameLevel LevelPrefab => _levelPrefab;
    }
}