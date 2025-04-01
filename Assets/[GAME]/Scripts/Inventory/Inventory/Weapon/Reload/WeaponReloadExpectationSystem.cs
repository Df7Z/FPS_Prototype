using System;
using ECS_MONO;
using Game.Player;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponReloadExpectationSystem : EcsSystemMono<ItemHotSlotView, WeaponReload, WeaponReloadExpectation>
    {
        protected override void Run(EntityMono e, ItemHotSlotView view, WeaponReload reload,
            WeaponReloadExpectation expectation)
        {
            if (expectation.Time > 0f)
            {
                expectation.Time -= Time.deltaTime;
                
                return;
            }

            TryReload(e, view, reload);
            
            e.Del<WeaponShootBlock>();
            e.Del<WeaponReloadExpectation>();
        }

        private void TryReload(EntityMono e, ItemHotSlotView view, WeaponReload reload)
        {
            //For player inventory
            var inventory = view.OwnerHotSlot.Owner.Get<Slot>().Collector;

            var runtime = reload.Owner.Get<WeaponReloadRuntime>();

            var result = inventory.TakeCountedItem(reload.AmmoType, reload.Max - runtime.Current);

            if (!result.Result)
            {
                throw new Exception("Reload break! Ammo take yet!");
            }

            runtime.SetCurrent(runtime.Current + result.Success);
        }
    }
}