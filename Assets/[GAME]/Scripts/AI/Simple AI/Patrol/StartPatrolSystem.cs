using ECS_MONO;
using UnityEngine;
using UnityEngine.AI;


namespace Game.AI
{
    internal sealed class PatrolSystem : EcsSystemMono<AITaskPatrol, AIAgent ,PatrolRuntime, Patrol>
    {
        protected override void Run(EntityMono e, AITaskPatrol task, AIAgent agent, PatrolRuntime runtime, Patrol patrol)
        {
            if (!agent.Agent.pathPending && agent.Agent.remainingDistance <= agent.Agent.stoppingDistance)
            {
                //wait time
                if (runtime.Time > 0f)
                {
                    runtime.Time -= Time.deltaTime;
                    
                    return;
                }
                
                e.Del<AITaskPatrol>();
            }
        }
    }

    internal sealed class StartPatrolSystem : EcsSystemMono<StartPatrolSignal, Patrol>
    {
        protected override void Run(EntityMono e, StartPatrolSignal c1, Patrol patrol)
        {
            var runtime = e.SafeAdd<PatrolRuntime>();

            runtime.Time = patrol.WaitTime;
            runtime.Target = GetRandomDestination(e, patrol);
            
            var agentTask = e.SafeAdd<AgentTask>();

            agentTask.SetDestination(runtime.Target);
            
            e.SafeAdd<AITaskPatrol>();
            
            e.Del<StartPatrolSignal>();
        }
        
        private Vector3 GetRandomDestination(EntityMono e, Patrol patrol)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrol.RadiusNewPoint;
            randomDirection += e.transform.position;

            if (NavMesh.SamplePosition(
                    randomDirection, 
                    out NavMeshHit hit, 
                    patrol.RadiusNewPoint, 
                    NavMesh.AllAreas))
            {
                Debug.DrawLine(e.transform.position, hit.position, Color.green, 2f);
                return hit.position;
            }

            Debug.DrawLine(e.transform.position, randomDirection, Color.red, 2f);
            return Vector3.zero;
        }
    }
}