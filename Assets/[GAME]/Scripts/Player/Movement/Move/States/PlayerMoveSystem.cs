using ECS_MONO;

namespace Game.Player.Move
{
    internal sealed class PlayerMoveSystemMono : EcsSystemMono<PlayerMovementRuntime, PlayerMovementView, PlayerInput>
    {
        protected override void Run(EntityMono e, PlayerMovementRuntime c1, PlayerMovementView c2, PlayerInput c3)
        {
            Move(e, c1, c2, c3);
        }

        private void Move(EntityMono e, PlayerMovementRuntime runtime, PlayerMovementView view, PlayerInput input)
        {
            if (view.CharacterController.isGrounded)
            {
                if (!e.Has<GroundMove>())
                {
                    e.Add<GroundMove>();
                 
                    e.SafeDel<AirMove>();
                }
            }
            else
            {
                if (!e.Has<AirMove>())
                {
                    e.Add<AirMove>();
                 
                    e.SafeDel<GroundMove>();
                }
            }
        }
    }
}