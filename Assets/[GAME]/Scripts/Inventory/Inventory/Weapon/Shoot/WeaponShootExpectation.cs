using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponShootExpectation : EcsComponent
    {
      
        public float Time;

        protected override void OnDespawnPool()
        {
            Time = 0f;
            
            Delete(this);
        }
    }
}