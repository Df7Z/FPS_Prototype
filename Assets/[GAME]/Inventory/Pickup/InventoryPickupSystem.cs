using System;
using ECS_MONO;
using UnityEditor;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class InventoryPickupSystem : EcsSystemMono<Item, PickupSignal>
    {
        protected override void Run(EntityMono e, Item c1, PickupSignal c2)
        {
            Pickup(c1, c2);

            e.Del(c2);
        }

        private void Pickup(Item item, PickupSignal signal)
        {
            var collector = signal.ItemCollector;

            foreach (var inventory in collector.Inventories)
            {
                if ((inventory.Owner.Has<HotInventory>() && item.Owner.Has<HotItem>()) ||
                    (inventory.Owner.Has<StorageInventory>() && item.Owner.Has<StorageItem>()))
                {
                    CollectToInventory(item, collector, inventory);
                }
            }
        }

        private void CollectToInventory(Item item, ItemCollector collector, Inventory inventory)
        {
            bool countedItem = item.Owner.Has<CountedItem>();

            foreach (var slot in inventory.Slots)
            {
                //if counted item
                if (countedItem && !slot.IsEmpty && slot.Item.ItemData.ID == item.ItemData.ID)
                {
                    var counter = item.Owner.Get<CountedItemRuntime>();
                    var slotCounter = slot.Item.Owner.Get<CountedItemRuntime>();

                    //Add count to exist slot with target type
                    if (slotCounter.TryAdd(counter.Current))
                    {
                        SetInSlot(item, slot);

                        return;
                    }
                }

                if (!slot.IsEmpty) continue;

                //Set item to slot
                slot.SetItem(item, collector);
                
                SetInSlot(item, slot);

                //If this slot is HotSlot and current active
                if (slot.Owner.Has<SelectedHot>())
                {
                    var signal = slot.Owner.Add<PickupInHotSlotSignal>();

                    signal.Inventory = inventory;
                }

                return;
            }

            Debug.Log("Inventory full!");

            return;
        }

        private void UpdateSlotUI(Slot slot)
        {
            if (slot.Item.Owner.Has<CountedItemRuntime>())
            {
                slot.Owner.Get<SlotCountView>().SetText(slot.Item.Owner.Get<CountedItemRuntime>().Current.ToString());
            }
        }

        private void SetInSlot(Item item, Slot slot)
        {
            //Disable map item view
            if (item.Owner.Has<ItemView>())
            {
                item.Owner.Get<ItemView>().ViewTransform.gameObject.SetActive(false);
            }

           // UpdateSlotUI(slot);
        }
    }
}