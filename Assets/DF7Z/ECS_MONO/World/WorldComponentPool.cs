using System;
using System.Collections.Generic;

namespace ECS_MONO
{
    internal class WorldComponentPool<E, C> where E :  class, IEntity where C : class, IEcsComponent
    {
        private EcsWorldAbstract<E> _world;
        private const bool _cacheAll = true;
        private Dictionary<Type, HashSet<C>> _pools; //Все компоненты по типу
        //private Dictionary<Type, HashSet<E>> _poolEntity; //Все сущьности по типу компонентов
        private const int DefaultSize = 4;
       
        public HashSet<T> GetComponents<T>()
        {
            if (!_pools.ContainsKey(typeof(T)))
            {
                CreatePool(typeof(T));
            }
            
            return _pools[typeof(T)] as HashSet<T>;

            //throw new Exception($"Cache component pool {typeof(T)} not exist in {_world.GetType()} !");
        }
        /*
        public HashSet<E> GetEntities<T>()
        {
            if (!_poolEntity.ContainsKey(typeof(T)))
            {
                CreatePool(typeof(T));
            }

            return _poolEntity[typeof(T)];
            
            //throw new Exception($"Cache entity pool {typeof(T)} not exist in {_world.GetType()} ! (Добавь компонент в пул мира)");
        }*/
        
        public WorldComponentPool(EcsWorldAbstract<E> world)
        {
            _world = world;
            _pools = new Dictionary<Type, HashSet<C>>();
            //_poolEntity = new Dictionary<Type, HashSet<E>>();
        }

        private void CreatePool(Type type)
        {
            _pools.Add(type, new HashSet<C>(DefaultSize));
            //_poolEntity.Add(type, new HashSet<E>(DefaultSize));
        }
       
        public void AddComponent(E entity, C component)
        {
            if (_cacheAll)
            {
                if (!_pools.ContainsKey(typeof(C)))
                {
                    _pools.Add(typeof(C), new HashSet<C>(DefaultSize));
                    //_poolEntity.Add(typeof(C), new HashSet<E>(DefaultSize));
                }

                _pools[typeof(C)].Add(component);
                //_poolEntity[typeof(C)].Add(entity);
            }
            else
            {
                if (_pools.ContainsKey(typeof(C)))
                {
                    _pools[typeof(C)].Add(component);
                   // _poolEntity[typeof(C)].Add(entity);
                }
            }
        }
        
        public void DeleteComponent(E entity, C component)
        {
            if (_pools.ContainsKey(typeof(C)))
            {
                _pools[typeof(C)].Remove(component);
                //_poolEntity[typeof(C)].Remove(entity);
            }
        }

    }
}