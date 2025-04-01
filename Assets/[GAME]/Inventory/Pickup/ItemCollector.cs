using System;
using System.Linq;
using ECS_MONO;
using Game.Level.Shared;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemCollector : EcsComponentMono
    {
        [SerializeField] private EntityReference _ownerInventory;
        
        [SerializeField] private float _radius = 2f;
        [SerializeField] private Transform _collectCenter;
        [SerializeField] private Inventory[] _inventories; //All inventories

        public EntityReference OwnerInventory => _ownerInventory;
        public Inventory[] Inventories => _inventories;
        public Transform CollectCenter => _collectCenter;
        public float Radius => _radius;

        protected override void OnRegisterEntity(IEntity entity)
        {
            _inventories = _inventories.OrderBy(inventory => inventory.PickupOrder).ToArray();
        }
    }
}