namespace Game.Mob.AI
{
    using UnityEngine;
    using UnityEngine.AI;

    public class RandomPatrol : MonoBehaviour
    {
        public float patrolRadius = 10f; // Радиус, в котором будут генерироваться случайные точки
        public float waitTime = 2f; // Время ожидания перед переходом к следующей точке
        private NavMeshAgent agent;
        private Vector3 targetPoint;
        private bool isWaiting = false;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            SetRandomDestination();
        }

        void Update()
        {
            // Если агент достиг точки назначения и не находится в процессе ожидания
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaiting)
            {
                StartCoroutine(WaitAndSetNewDestination());
            }
        }

        private void SetRandomDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                targetPoint = hit.position;
                agent.SetDestination(targetPoint);
            }
        }

        private System.Collections.IEnumerator WaitAndSetNewDestination()
        {
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            isWaiting = false;
            SetRandomDestination();
        }
    }
}