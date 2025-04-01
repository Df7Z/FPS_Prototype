using System;
using System.Collections.Generic;

namespace ECS_MONO
{
    internal sealed class PoolController<E> where E : class, IEntity
    {
        private Dictionary<Type, IEcsWorldComponentPoolBase> _pools;

        public PoolController(Dictionary<Type, IEcsWorldComponentPoolBase> pools)
        {
            _pools = pools;
        }

        public void EntityDelComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            if (component != null)
            {
                if (_pools.ContainsKey(typeof(T)))
                {
                    _pools[typeof(T)].Remove(component);
                }
            }
        }

        public T GetFirstWorldComponent<T>() where T : class, IEcsComponent
        {
            return (T) GetPool<T>().GetFirstComponent();
        }
        
        public IEcsWorldComponentPool<T> GetPool<T>() where T : class, IEcsComponent
        {
            if (_pools.TryGetValue(typeof(T), out IEcsWorldComponentPoolBase pool))
            {
                return (IEcsWorldComponentPool<T>)pool;
            }

            throw new Exception("Pool not created! Show InitPools method!");
        }

        public void EntityAddComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            if (component != null)
            {
                if (_pools.ContainsKey(typeof(T)))
                {
                    _pools[typeof(T)].Add(component);
                }
            }
        }

        public void EntityAddToWorld(E entity)
        {
            foreach (var type in entity.Types)
            {
                if (_pools.ContainsKey(type))
                {
                    _pools[type].Add(entity.Get(type));
                }
            }
        }

        public void EntityDelFromWorld(E entity)
        {
            foreach (var type in entity.Types)
            {
                if (_pools.ContainsKey(type))
                {
                    _pools[type].Remove(entity.Get(type));
                }
            }
        }
    }
}