using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    internal sealed class PlayerDefaultInputSystemMono : EcsSystemMono<PlayerInput, InputDefaultState>
    {
        protected override void Run(EntityMono e, PlayerInput input, InputDefaultState c2)
        {
            GetJump(input);
            GetInventory(input);
            GetCrouh(input);
            GetMouseAxis(input);
            GetDirectionAxis(input);
            
            input.SetMouseWheel(Input.GetAxis("Mouse ScrollWheel"));
            
            input.SetNumDown(0, Input.GetKeyDown(KeyCode.Alpha1));
            input.SetNumDown(1, Input.GetKeyDown(KeyCode.Alpha2));
            input.SetNumDown(2, Input.GetKeyDown(KeyCode.Alpha3));
            input.SetNumDown(3, Input.GetKeyDown(KeyCode.Alpha4));
            input.SetNumDown(4, Input.GetKeyDown(KeyCode.Alpha5));
            input.SetNumDown(5, Input.GetKeyDown(KeyCode.Alpha6));
            input.SetNumDown(6, Input.GetKeyDown(KeyCode.Alpha7));
            input.SetNumDown(7, Input.GetKeyDown(KeyCode.Alpha8));
            input.SetNumDown(8, Input.GetKeyDown(KeyCode.Alpha9));
            input.SetNumDown(9, Input.GetKeyDown(KeyCode.Alpha0));
            
            input.SetIsLeftMouseButton(Input.GetKey(KeyCode.Mouse0));
            
            input.SetIsRightMouseButton(Input.GetKey(KeyCode.Mouse1));
            input.SetIsRightMouseButtonDown(Input.GetKeyDown(KeyCode.Mouse1));
            input.SetIsRightMouseButtonUp(Input.GetKeyUp(KeyCode.Mouse1));
            
            input.SetIsReload(Input.GetKey(KeyCode.R));
        }

        private void GetInventory(PlayerInput input)
        {
            input.SetInventoryDown(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Q));
        }

        private void GetJump(PlayerInput input)
        {
            input.SetJump(Input.GetKey(KeyCode.Space));
            input.SetJumpUp(Input.GetKeyUp(KeyCode.Space));
            input.SetJumpDown(Input.GetKeyDown(KeyCode.Space));
        }

        private void GetCrouh(PlayerInput input) => input.SetCrouch(Input.GetKey(KeyCode.C));
        
        private void GetMouseAxis(PlayerInput input) => input.SetAxis(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        private void GetDirectionAxis(PlayerInput input)
        {
            input.SetAxisRaw(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            input.SetAxisRawNormalized(input.AxisRaw.normalized);
        }
    }
}