using ECS_MONO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game.AI
{
    internal class AIAgent : EcsComponentMono
    {
        [SerializeField] private NavMeshAgent _agent;

        public NavMeshAgent Agent => _agent;

        protected override void OnDespawnPool()
        {
            _agent.enabled = false;
        }

        protected override void OnSpawnPool()
        {
            _agent.enabled = true;
        }

        public void Activate() => _agent.enabled = true;
    }
}