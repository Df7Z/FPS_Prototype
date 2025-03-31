using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponReloadSystem : EcsSystemMono<ItemHotSlotView, WeaponReload, ReloadInteractSignal>
    {
        protected override void Run(EntityMono e, ItemHotSlotView view, WeaponReload reload, ReloadInteractSignal signal)
        {
            e.Del<ReloadInteractSignal>();
            
            if (e.Has<WeaponReloadExpectation>()) return;
            
            var inventory = view.OwnerSlot.Collector;

            var runtime = reload.Owner.Get<WeaponReloadRuntime>();

            if (runtime.Current >= reload.Max)
            {
                Debug.Log("Max mag ammo!");
                
                return;
            }
            
            var result = inventory.CanTakeCountedItem(reload.AmmoType, 1);

            if (!result.Result)
            {
                Debug.Log("No ammo!");
                
                return; 
            }
            
            e.Add<WeaponShootBlock>();
            
            if (reload.Owner.TryGet(out EntityAnimator animator))
            {
                animator.Play(reload.Animation);
            }
            
            var expectation = e.Add<WeaponReloadExpectation>();

            expectation.Time = reload.Time;
        }
    }
}