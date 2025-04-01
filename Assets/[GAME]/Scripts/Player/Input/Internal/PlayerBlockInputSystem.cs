using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    internal sealed class PlayerBlockInputSystemMono : EcsSystemMono<PlayerInput, InputBlockState>
    {
        protected override void Run(EntityMono e, PlayerInput c1, InputBlockState c2)
        {
            c1.SetJump(false);
            c1.SetJumpUp(false);
            c1.SetJumpDown(false);
            
            c1.SetInventoryDown(false);
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
        }
    }
}