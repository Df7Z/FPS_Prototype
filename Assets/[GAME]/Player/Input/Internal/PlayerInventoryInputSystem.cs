using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    internal sealed class PlayerInventoryInputSystem : EcsSystemMono<PlayerInput, InputInventoryState>
    {
        protected override void Run(EntityMono e, PlayerInput c1, InputInventoryState c2)
        {
            c1.SetJump(false);
            c1.SetJumpUp(false);
            c1.SetJumpDown(false);
            c1.SetCrouch(false);
            c1.SetAxis(0f, 0f);
            c1.SetAxisRaw(0f, 0f);
            c1.SetAxisRawNormalized(Vector2.zero);
            c1.SetNumDowns();
            c1.SetMouseWheel(0f);
            c1.SetIsLeftMouseButton(false);
            c1.SetIsRightMouseButton(false);
            c1.SetIsRightMouseButtonDown(false);
            c1.SetIsRightMouseButtonUp(false);
            c1.SetIsReload(false);
            
            GetInventory(c1);
        }
        
        private void GetInventory(PlayerInput input)
        {
            input.SetInventoryDown(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Q));
        }
    }
}