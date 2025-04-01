using System;
using ECS_MONO;
using Game.Player;
using PoolSystem;
using UnityEngine;

namespace Game.Inventory
{
    internal class HotSlotSwitchSystem : EcsSystemMono<HotInventory, Inventory>
    {
        private PlayerInput _playerInput;
        private IEcsWorldComponentPool<PlayerInput> _pool;

        protected override void Run(EntityMono e, HotInventory hotInventory, Inventory inventory)
        {
            if (_playerInput == null) return;

            TrySwitch(e, hotInventory, inventory, _playerInput);
        }

        private void TrySwitch(EntityMono e, HotInventory hotInventory, Inventory inventory, PlayerInput input)
        {
            if (_playerInput.MouseWheel > 0.01f)
            {
                var current = CurrentSelected(inventory);
                
                if (current >= inventory.Slots.Length - 1) SwitchToSlot(0, inventory, hotInventory);
                else SwitchToSlot(current + 1, inventory, hotInventory);
            }
            
            if (_playerInput.MouseWheel < -0.01f)
            {
                var current = CurrentSelected(inventory);
                
                if (current <= 0) SwitchToSlot(inventory.Slots.Length - 1, inventory, hotInventory);
                else SwitchToSlot(current - 1, inventory, hotInventory);
            }

            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (i > input.NumDowns.Length) break;
                
                if (input.NumDowns[i])
                {
                    SwitchToSlot(i, inventory, hotInventory);
                    
                    return;
                }
            }
        }

        private int CurrentSelected(Inventory inventory)
        {
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.Slots[i].Owner.Has<SelectedHot>()) return i;
            }

            throw new Exception("Selected Hot slot not found!");
        }
        
        private void SwitchToSlot(int index, Inventory inventory, HotInventory hotInventory)
        {
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (i == index)
                {
                    ActivateHotSlot(inventory.Slots[i].Owner.Get<HotSlot>(), hotInventory);
                    
                    if (inventory.Slots[i].Owner.Has<SelectedHot>()) continue;
                    
                    inventory.Slots[i].Owner.Add<SelectedHot>();
                }
                else
                {
                    var slot = inventory.Slots[i].Owner.Get<HotSlot>();
                    
                    slot.SetActive(false);

                    if (slot.HasView)
                    {
                        SystemPool.Despawn(slot.HotView.gameObject);

                        slot.SetView(null);
                    }
                    
                    inventory.Slots[i].Owner.SafeDel<SelectedHot>();
                }
            }
        }

        private void ActivateHotSlot(HotSlot hotSlot, HotInventory hotInventory)
        {
            var slot = hotSlot.Owner.Get<Slot>();
            
            hotSlot.SetActive(true);

            if (hotSlot.HasView)
            {
                var prefab = slot.Item.Owner.Get<HotItem>().PrefabView;

                if (hotSlot.HotView.GetType() != prefab.GetType()) throw new Exception();
               
                return;
            }

            if (slot.IsEmpty) return;

            var signal = slot.Owner.Add<CreateViewForHotSlotSignal>();

            signal.HotInventory = hotInventory;
        }

        protected override void OnCoreInitialized()
        {
            var world = GetWorld(WorldId.Player);
            
            _pool = world.GetPool<PlayerInput>();
            
            _pool.OnAdd += OnAddPlayerInput;
            _pool.OnRemove += OnRemovePlayerInput;
            
            var input= world.GetAnyEntityWith<PlayerInput>();
            if (input != null)
            {
                _playerInput = input.Get<PlayerInput>();
            }
        }

        protected override void OnDestruct()
        {
            _pool.OnAdd -= OnAddPlayerInput;
            _pool.OnRemove -= OnRemovePlayerInput;
        }
        
        private void OnRemovePlayerInput(PlayerInput component) => _playerInput = null;
        private void OnAddPlayerInput(PlayerInput component) => _playerInput = component;
    }
}