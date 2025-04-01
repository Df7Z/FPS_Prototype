using ECS_MONO;
using UnityEngine;

namespace Game.Mob.AI
{
    internal sealed class AgentTask : EcsComponent
    {
        private Vector3 _destination;

        public Vector3 Destination => _destination;

        public void SetDestination(Vector3 value) => _destination = value;
    }
}