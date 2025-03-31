using ECS_MONO;
using Game.Damage;
using UnityEngine;

namespace Game.Inventory.HealthPack
{
    internal sealed class HealthPack : EcsComponentMono
    {
        [SerializeField] private HealData _heal;

        public HealData Heal => _heal;
    }
}