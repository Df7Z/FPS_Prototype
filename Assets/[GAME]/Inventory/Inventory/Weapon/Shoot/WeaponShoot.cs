using ECS_MONO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Inventory
{
    internal sealed class WeaponShoot : EcsComponentMono
    {
        [SerializeField] [Min(0f)] private float _fireRate = 0.2f;
        [SerializeField] [Min(1f)] private float _distance = 100f;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private Transform _flashSpawnPoint;
        [SerializeField] private GameObject _flashPrefab;
        [SerializeField] private EntityAnimation _animation;
        public EntityAnimation Animation => _animation;
        public float FireRate => _fireRate;
        public float Distance => _distance;
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
        public Transform FlashSpawnPoint => _flashSpawnPoint;
        public GameObject FlashPrefab => _flashPrefab;
        
        private void Awake()
        {
            _animation.MakeHash();
        }
    }
}