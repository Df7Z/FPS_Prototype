using System;
using ECS_MONO;

namespace Game.Player
{
    public sealed class ChangeInputStateSignal : EcsComponent
    {
        public PlayerInputState Target = PlayerInputState.Default;
    }
}