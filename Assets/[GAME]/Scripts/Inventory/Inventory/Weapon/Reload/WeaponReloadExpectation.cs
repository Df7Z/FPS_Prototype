using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponReloadExpectation : EcsComponent
    {
        public float Time;
        
        protected override void OnDespawnPool()
        {
            Time = 0f;
            
            Delete(this);
        }
    }
}