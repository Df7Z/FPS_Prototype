using ECS_MONO;
using Game.Damage;
using UnityEngine;

namespace Game.AI
{
    internal class MeleeAttack : EcsComponentMono
    {
        [SerializeField] private DamageData _damage;
        [SerializeField] private float _time = 0.75f;
        [SerializeField] private float _minDistance = 1.5f;

        public float Time => _time;
        public DamageData Damage => _damage;
        public float MinDistance => _minDistance;
    }
}