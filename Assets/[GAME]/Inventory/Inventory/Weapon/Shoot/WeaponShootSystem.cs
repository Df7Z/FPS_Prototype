using ECS_MONO;
using Game.Projectile.Shared;
using PoolSystem;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponShootSystem : EcsSystemMono<ItemHotSlotView, WeaponShoot, PrimaryInteractSignal>
    {
        protected override void Run(EntityMono e, ItemHotSlotView view, WeaponShoot shoot, PrimaryInteractSignal signal)
        {
            e.Del<PrimaryInteractSignal>();
            
            if (e.Has<WeaponShootExpectation>()) return;
            if (e.Has<WeaponShootBlock>()) return;
            
            TryShoot(e, view, shoot);
        }

        private void TryShoot(EntityMono e, ItemHotSlotView view, WeaponShoot shoot)
        {
            if (e.Has<WeaponReload>())
            {
                var reload = e.Get<WeaponReload>();
                var runtime = e.Get<WeaponReloadRuntime>();

                if (runtime.Current > 0)
                {
                    DoShoot(e, view, shoot);
                    
                    //Take ammo
                    runtime.Take(1);
                }
                else
                {
                    TryReload(e ,reload);
                }
            }
            else
            {
                DoShoot(e, view, shoot);
            }
        }

        private void TryReload(EntityMono e, WeaponReload reload)
        {
            if (e.Has<ReloadInteractSignal>()) return;
            
            e.Add<ReloadInteractSignal>();
        }

        private void DoShoot(EntityMono e, ItemHotSlotView view, WeaponShoot shoot)
        {
            if (shoot.Owner.TryGet(out EntityAnimator animator))
            {
                if (shoot.Animation.HasClips) animator.Play(shoot.Animation);
            }

            shoot.RaycastProjectileData.CreateProjectile(
                view.OwnerSlot.Collector.OwnerInventory.Entity,
                shoot.ProjectileSpawnPoint);
            
            SystemPool.Spawn(shoot.FlashPrefab, shoot.FlashSpawnPoint.position, shoot.FlashSpawnPoint.rotation);

            var wait = e.Add<WeaponShootExpectation>();

            wait.Time = shoot.FireRate;
        }
    }
}