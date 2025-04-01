using ECS_MONO;
using UnityEngine;

namespace Game.Inventory.Shared
{
    public sealed class PlayerInventory : EcsComponentMono
    {
        [SerializeField] private EntityMono _collector;

        public EntityMono Collector => _collector;
    }
}