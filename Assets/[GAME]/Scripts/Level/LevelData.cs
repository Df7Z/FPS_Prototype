using UnityEngine;

namespace Game.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "Game/Level/SD")]
    internal class LevelData : ScriptableObject
    {
        [SerializeField] private GameLevel _levelPrefab;

        public GameLevel LevelPrefab => _levelPrefab;
    }
}