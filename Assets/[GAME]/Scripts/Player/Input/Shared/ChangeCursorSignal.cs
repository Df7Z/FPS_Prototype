using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    public class ChangeCursorSignal : EcsComponent
    {
        public CursorLockMode Target = CursorLockMode.None;
        
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}