using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ECS_MONO
{
    internal static class ComponentPool
    {
        private static Dictionary<Type, IPoolComponent> _pools;

        static ComponentPool()
        {
            _pools = new Dictionary<Type, IPoolComponent>(16);
        }
        
        public static T Give<T>() where T : class, IEcsComponent, new()
        {
            var type = typeof(T);

            CheckPool<T>();

            return _pools[type].Get<T>();
        }
        
        
        public static void ReturnNoNew<T>(T value) where T : class, IEcsComponent
        {
            var type = typeof(T);
          
            if (!_pools.ContainsKey(type)) return;
            
            _pools[type].ReturnNoNew(value);
        }
        

        
        public static void Return<T>(T value) where T : class, IEcsComponent, new()
        {
            var type = typeof(T);

            CheckPool<T>();
            
            _pools[type].Return(value);
        }
        
        private static void CheckPool<T>()where T : class, IEcsComponent, new()
        {
            if (!_pools.ContainsKey(typeof(T)))
            {
                var pool = new ObjectPoolComponent<T>(() => new T());
                _pools.Add(typeof(T), pool);
            }

            if (_pools[typeof(T)] == null)
            {
                var pool = new ObjectPoolComponent<T>(() => new T());
                _pools[typeof(T)] = pool;
            }
        }
      
    }

    public interface IPoolComponent
    {
        T Get<T>() where T : class, IEcsComponent, new();
        void Return<T>(T value) where T : class, IEcsComponent, new();
        void ReturnNoNew<T>(T value) where T : class, IEcsComponent;
    }
    
    internal class ObjectPoolComponent<T> : IPoolComponent where T : class, IEcsComponent, new()
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;

        public ObjectPoolComponent(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }
        
        public ObjectPoolComponent()
        {
            _objectGenerator = () => new T();
            _objects = new ConcurrentBag<T>();
        }


        private object GetObject<T1>()
        {
            if (_objects.TryTake(out T item))
            {
                return item;
            }
           
            return _objectGenerator();
        }

        private void ReturnTo(T item) => _objects.Add(item);

        public T1 Get<T1>() where T1 : class, IEcsComponent, new()
        {
            return (T1) GetObject<T>();
        }

        public void Return<T1>(T1 value) where T1 : class, IEcsComponent, new()
        {
            ReturnTo((T) (object) value);
        }

        public void ReturnNoNew<T1>(T1 value) where T1 : class, IEcsComponent
        {
            ReturnTo((T) (object) value);
        }
    }
    
   
}