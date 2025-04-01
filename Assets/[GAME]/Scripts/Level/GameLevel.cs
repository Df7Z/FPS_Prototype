using System;
using ECS_MONO;
using Game.Level.Shared;
using Game.Player;
using UnityEngine;

namespace Game.Level
{
    internal class GameLevel : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawn;

        private EntityMono _player;

        private LevelRespawnObjects _levelRespawnObjects;
        
        private void Awake()
        {
            _levelRespawnObjects = new LevelRespawnObjects();
            _levelRespawnObjects.Init(gameObject.GetComponentsInChildren<LevelRespawnObject>());
        }

        public void SpawnPlayer(EntityMono playerPrefab)
        {
            _player = Instantiate(playerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        }

        public void Restart(LevelRestartParams restartParams)
        {
            _levelRespawnObjects.Restart(_levelRespawnObjects.RespawnObject.GetComponentsInChildren<LevelRespawnObject>());
            
            var signal = _player.Add<LevelRestartSignal>();

            signal.RestartParams = restartParams;
            signal.PlayerSpawn = _playerSpawn;
        }
    }
}