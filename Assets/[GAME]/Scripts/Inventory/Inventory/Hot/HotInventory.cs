using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class HotInventory : EcsComponentMono //Инвентарь горячих слотов
    {
        [SerializeField] private Transform _spawnHotItemView;

        public Transform SpawnHotItemView => _spawnHotItemView;
    }
}