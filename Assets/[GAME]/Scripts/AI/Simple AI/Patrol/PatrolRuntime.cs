using ECS_MONO;
using UnityEngine;

namespace Game.AI
{
    internal sealed class PatrolRuntime : EcsComponent
    {
        public Vector3 Target;
        public float Time;
    }
}