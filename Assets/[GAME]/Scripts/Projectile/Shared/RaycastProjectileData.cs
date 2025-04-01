using Game.Damage;
using UnityEngine;

namespace Game.Projectile.Shared
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Game/Projectile/Raycast SD")]
    public class RaycastProjectileData : ProjectileData
    {
        [SerializeField] protected float _maxDistance = 100f;
        [SerializeField] protected DamageData _damageData;

        public DamageData DamageData => _damageData;
        public float MaxDistance => _maxDistance;
    }
}