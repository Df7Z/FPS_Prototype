using PoolSystem;
using UnityEngine;

namespace ECS_MONO.Level
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawn;

        private EntityMono _player;
        
        public void SpawnPlayer(EntityMono playerPrefab)
        {
            _player = Instantiate(playerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        }
        
    }
}