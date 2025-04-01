using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MONO
{
    public abstract class EcsTime
    {
        private float _globalMod = 1f;
    }
    
    public sealed class EcsSystemTime : EcsTime
    {
    }

    public sealed class EcsWorldTime<E> where E : class, IEntity, new()
    {
        private Dictionary<Type, EcsSystemTime> _times;

        public bool SetTimeForSystem<T>(float factor) where T : IEcsSystem<E>
        {
            if (!_times.ContainsKey(typeof(T))) return false;

            var time = _times[typeof(T)];
            
            return true;
        }
    }
}