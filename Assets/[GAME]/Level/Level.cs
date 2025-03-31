using UnityEngine;

namespace ECS_MONO.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private EntityMono _playerPrefab;
        [SerializeField] private LevelData[] _levels;

        private GameLevel _level;
        
        #region SINGLETONE

        public static Level I;

        private void Awake() {
            SINGLETONE();
        }
    
        private void SINGLETONE() {
            if (I == null) {
                I = this;
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                
                Init();
            }
            else if (I != this) {
                Destroy(gameObject);
            }
        }
        
        #endregion

        private void Init()
        {
            //Проверка сохранений
            
            //Создание уровня по умолчанию
            LoadLevel(_levels[0]);
        }

        private void LoadLevel(LevelData data)
        {
            _level = Instantiate(data.LevelPrefab);

            _level.SpawnPlayer(_playerPrefab);
        }
    }
}