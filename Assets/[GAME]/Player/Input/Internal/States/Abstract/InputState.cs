using ECS_MONO;

namespace Game.Player
{
    internal abstract class InputState : EcsComponent
    {
        public abstract PlayerInputState ID { get; }
    }
}