﻿using System;
using ECS_MONO;
using PoolSystem;

namespace Game.Inventory
{
    internal sealed class Slot : EcsComponent
    {
        private Item _item;
        private ItemCollector _collector;

        public ItemCollector Collector => _collector;

        public Item Item => _item;
        
        public void SetItem(Item entity, ItemCollector collector)
        {
            _item = entity;
            _collector = collector;

            //Do item child collector 
            _item.transform.parent = _collector.transform; 
          
            UpdateView();
        }

        public void UpdateView()
        {
            if (_item != null)
            {
                if (_item.Owner.Has<CountedItemRuntime>())
                {
                    if (Owner.Has<SlotCountView>())
                    {
                        Owner.Get<SlotCountView>().SetText(_item.Owner.Get<CountedItemRuntime>().Current.ToString());
                    }
                }
                
                if (Owner.Has<SlotView>())
                {
                    var slotView = Owner.Get<SlotView>();
                    
                    slotView.UpdateIcon(_item.ItemData.Icon);
                }
            }
            else
            {
                if (Owner.Has<SlotCountView>())
                {
                    Owner.Get<SlotCountView>().SetText(string.Empty);
                }
                
                if (Owner.Has<SlotView>())
                {
                    var slotView = Owner.Get<SlotView>();
                    
                    slotView.Clear();
                }
            }
        }
        
        public bool IsEmpty => _item == null;

        public void Clear()
        {
            SystemPool.Despawn(_item.gameObject);
            
            _item = null;
            
            UpdateView();
        }
    }
}