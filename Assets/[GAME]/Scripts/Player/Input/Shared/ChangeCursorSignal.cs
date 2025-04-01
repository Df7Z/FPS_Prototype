using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    public class ChangeCursorSignal : EcsComponent
    {
        public CursorLockMode Target = CursorLockMode.None;
        
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}