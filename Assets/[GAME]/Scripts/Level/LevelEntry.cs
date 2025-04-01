using System;
using ECS_MONO;
using Game.Level.Shared;
using UnityEngine;

namespace Game.Level
{

    public class LevelEntry : MonoBehaviour
    {
        [SerializeField] private EntityMono _playerPrefab;
        [SerializeField] private LevelData[] _levels;

        private GameLevel _level;
        
        #region SINGLETONE

        public static LevelEntry I;

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
            LevelEventBus.OnPlayerRespawnRequest += OnPlayerRespawnRequest;
            
            //Проверка сохранений
            
            //Создание уровня по умолчанию
            LoadLevel(_levels[0]);
        }

        private void OnPlayerRespawnRequest(IEntity e)
        {
            //reset level

            LevelRestartParams restartParams = new LevelRestartParams();
            
            _level.Restart(restartParams);
            
            LevelEventBus.OnLevelRestart?.Invoke(restartParams);
            
        }

        private void LoadLevel(LevelData data)
        {
            _level = Instantiate(data.LevelPrefab);

            _level.SpawnPlayer(_playerPrefab);
        }

        private void OnDestroy()
        {
            LevelEventBus.OnPlayerRespawnRequest -= OnPlayerRespawnRequest;
        }
    }
}