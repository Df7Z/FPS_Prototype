using System;
using System.Collections.Generic;
using ECS_MONO;
using Game.Inventory.HealthPack;
using Game.Inventory.Shared;
using UnityEditor;

namespace Game.Inventory
{
    public sealed class InventoryWorld : EcsWorldMono
    {
        public override WorldId ID => WorldId.Inventory;
        protected override void InitSystems()
        {
            CreateUpdateSystem<InventoryInitializerSystem>();
            
            CreateUpdateSystem<PlayerResetInventorySystem>();
            
            CreateUpdateSystem<ClearSlotSystem>();
            
            CreateUpdateSystem<InventoryChangeViewSystem>();
            CreateUpdateSystem<HotSlotSwitchSystem>();
            CreateUpdateSystem<HandleInteractHotSlotSystem>();
            
            CreateUpdateSystem<InventoryPickupSystem>();
            CreateUpdateSystem<PickupInHotSlotSystem>();
            CreateUpdateSystem<CreateViewForHotSlotSystem>();

            CreateUpdateSystem<InteractSlotSystem>();
            
            CreateUpdateSystem<WeaponShootSystem>();
            CreateUpdateSystem<WeaponShootExpectationSystem>();
            CreateUpdateSystem<WeaponReloadSystem>();
            CreateUpdateSystem<WeaponReloadExpectationSystem>();
            
            CreateUpdateSystem<HealthPackInteractSystem>();
            
            CreateUpdateSystem<ItemSwaySystem>();
            
            CreateFixedUpdateSystem<ItemCollectorSystem>();
            CreateLateUpdateSystem<ChangeCanCollectSystem>();

        }

        protected override void InitPools(in Dictionary<Type, IEcsWorldComponentPoolBase> p)
        {
            p.Add(typeof(PlayerInventory), new EcsWorldComponentPool<PlayerInventory>());
        }
    }
}