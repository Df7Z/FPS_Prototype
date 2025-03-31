using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal static class ItemCollectorFunctions
    {
        public static int GetCount(this ItemCollector collector, ItemId id)
        {
            int result = 0;
            
            foreach (var inventory in collector.Inventories)
            {
                foreach (var slot in inventory.Slots)
                {
                    if (slot.IsEmpty) continue;
                    
                    if (slot.Item.ItemData.ID == id)
                    {
                        if (EcsGlobalSetup.IsDebug && !slot.Item.Owner.Has<CountedItem>()) 
                            throw new Exception("Not counted item");
                        
                        result += slot.Item.Owner.Get<CountedItemRuntime>().Current;
                    }
                }
            }

            return result;
        }

        public static ItemInventoryProcedure CanTakeCountedItem(this ItemCollector collector, ItemId id, int count)
        {
            foreach (var inventory in collector.Inventories)
            {
                var result = CanTakeCountedItem(id, count, inventory);
                
                if (result.Result) return result;
            }

            return new ItemInventoryProcedure();
        }
        
        private static ItemInventoryProcedure CanTakeCountedItem(ItemId id, int count, Inventory inventory)
        {
            ItemInventoryProcedure result = new ItemInventoryProcedure();
            
            foreach (var slot in inventory.Slots)
            {
                if (slot.IsEmpty) continue;
                
                if (slot.Item.ItemData.ID == id) //Only see in one slot (simple logic)
                {
                    if (EcsGlobalSetup.IsDebug && !slot.Item.Owner.Has<CountedItem>()) 
                        throw new Exception("Not counted item");

                    var runtime = slot.Item.Owner.Get<CountedItemRuntime>();

                    if (runtime.Current < count)
                    {
                        result.Result = true;
                        result.Remain = count - runtime.Current;
                        result.Success = count;
                        
                        return result;
                    }
                    
                    if (runtime.Current >= count)
                    {
                        result.Result = true;
                        result.Remain = 0;
                        result.Success = count;

                        return result;
                    }
                }
            }

            return result;
        }        
        
        public static ItemInventoryProcedure TakeCountedItem(this ItemCollector collector, ItemId id, int count)
        {
            foreach (var inventory in collector.Inventories)
            {
                var result = TakeCountedItem(id, count, inventory);
                
                if (result.Result) return result;
            }

            return new ItemInventoryProcedure();
        }

        private static ItemInventoryProcedure TakeCountedItem(ItemId id, int count, Inventory inventory)
        {
            ItemInventoryProcedure result = new ItemInventoryProcedure();
            
            foreach (var slot in inventory.Slots)
            {
                if (slot.IsEmpty) continue;
                
                if (slot.Item.ItemData.ID == id) //Only see in one slot (simple logic)
                {
                    if (EcsGlobalSetup.IsDebug && !slot.Item.Owner.Has<CountedItem>()) 
                        throw new Exception("Not counted item");

                    var runtime = slot.Item.Owner.Get<CountedItemRuntime>();

                    if (runtime.Current == 0)
                    {
                        Debug.LogError("CountedItemRuntime count == 0!");
                        
                        continue;
                    }
                    
                    if (runtime.Current < count)
                    {
                        result.Result = true;
                        result.Remain = count - runtime.Current;
                        result.Success = runtime.Current;

                        UpdateCountedItem(slot, runtime, 0);
                        CheckCountedItemSlotEmpty(slot, runtime);
                        
                        return result;
                    }
                    
                    if (runtime.Current >= count)
                    {
                        UpdateCountedItem(slot, runtime, runtime.Current - count);
                        
                        result.Result = true;
                        result.Remain = 0;
                        result.Success = count;

                        CheckCountedItemSlotEmpty(slot, runtime);
                        
                        return result;
                    }
                }
            }

            return result;
        }

        private static bool CheckCountedItemSlotEmpty(Slot slot, CountedItemRuntime runtime)
        {
            if (runtime.Current == 0)
            {
                //Clear slot
                slot.Clear();
                
                UpdateSlotView(slot);
                
                return true;
            }

            return false;
        }

        private static void UpdateSlotView(Slot slot)
        {
            if (slot.IsEmpty)
            {
                if (slot.Owner.Has<SlotView>())
                {
                    var view = slot.Owner.Get<SlotView>();
                    
                    view.Clear();
                }
            }
        }

        private static void UpdateCountedItem(Slot slot, CountedItemRuntime runtime, int newValue)
        {
            runtime.Current = newValue;
            
            slot.Owner.Get<SlotCountView>().
                SetText(
                    newValue == 0 ?
                    string.Empty :
                    runtime.Current.ToString());
        }
    }
}