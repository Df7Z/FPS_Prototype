using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemCollector : EcsComponentMono
    {
        [SerializeField] private EntityReference _ownerInventory;
        
        [SerializeField] private float _radius = 2f;
        [SerializeField] private Transform _collectCenter;
        
        [SerializeField] private StorageInventory _storageInventory;
        [SerializeField] private HotInventory _hotInventory;
        
        [SerializeField] private Inventory[] _inventories; //All inventories

        public EntityReference OwnerInventory => _ownerInventory;
        public Inventory[] Inventories => _inventories;
        public StorageInventory StorageInventory => _storageInventory;
        public HotInventory HotInventory => _hotInventory;
        public Transform CollectCenter => _collectCenter;
        public float Radius => _radius;

       
    }
}