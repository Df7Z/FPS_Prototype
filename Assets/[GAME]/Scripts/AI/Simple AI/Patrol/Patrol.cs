using ECS_MONO;
using UnityEngine;

namespace Game.AI
{
    internal sealed class Patrol : EcsComponentMono
    {
        [SerializeField] private float _radiusNewPointMin = 6f;
        [SerializeField] private float _radiusNewPointMax = 16f;
        [SerializeField] private float _waitTime = 2f;

        public float RadiusNewPoint => Random.Range(_radiusNewPointMin, _radiusNewPointMax);

        public float WaitTime => _waitTime;
    }
}