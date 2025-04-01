using System;
using ECS_MONO;

namespace Game.Player.Look
{
    [Serializable]
    internal sealed class PlayerMouseLookRuntime : EcsComponent
    {
        public float Sens = 2f;
        
        [NonSerialized] public float XRotation;
        [NonSerialized] public float YRotation;
    }
}