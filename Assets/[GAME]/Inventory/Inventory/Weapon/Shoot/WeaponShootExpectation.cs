using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponShootExpectation : EcsComponent
    {
      
        public float Time;

        public override void OnDespawnPool()
        {
            Time = 0f;
            
            Delete(this);
        }
    }
}