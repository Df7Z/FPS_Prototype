using ECS_MONO;

namespace Game.Player.Move
{
    internal sealed class PlayerMoveCameraSystemMono : EcsSystemMono<PlayerMovementView>
    {
        protected override void Run(EntityMono e, PlayerMovementView view)
        {
            view.ViewMouseLook.position = view.ViewTransform.position;
        }
    }
}