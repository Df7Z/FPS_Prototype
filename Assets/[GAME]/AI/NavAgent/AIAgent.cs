using ECS_MONO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game.Mob.AI
{
    internal class AIAgent : EcsComponentMono
    {
        [SerializeField] private NavMeshAgent _agent;

        public NavMeshAgent Agent => _agent;

        public override void OnDespawnPool(IEntity entity)
        {
            _agent.enabled = false;
        }

        public override void OnSpawnPool(IEntity entity)
        {
            _agent.enabled = true;
        }

        public void Activate() => _agent.enabled = true;
    }
}