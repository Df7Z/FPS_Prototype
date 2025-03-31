using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponShootExpectationSystem : EcsSystemMono<ItemHotSlotView, WeaponShoot, WeaponShootExpectation>
    {
        protected override void Run(EntityMono e, ItemHotSlotView view, WeaponShoot shoot, WeaponShootExpectation expectation)
        {
            if (expectation.Time < 0f)
            {
                e.Del<WeaponShootExpectation>();
                
                return;
            }

            expectation.Time -= Time.deltaTime;
        }
    }
}