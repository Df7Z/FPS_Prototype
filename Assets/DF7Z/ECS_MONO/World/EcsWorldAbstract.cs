using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MONO
{
    public abstract class EcsWorldAbstract<E> : MonoBehaviour, IEcsWorld<E> where E : class, IEntity
    {
        private IEcsCore _core;
        public abstract WorldId ID { get; }

        private HashSet<E> _entities = new HashSet<E>(256);

        private List<IEcsSystem<E>> _systems;
        private List<IEcsSystem<E>> _updateSystems;
        private List<IEcsSystem<E>> _fixedSystems;
        private List<IEcsSystem<E>> _lateSystems;

        private WorldComponentPool<E, EcsComponent> _worldComponentPool;
        private WorldComponentPool<E, EcsComponentMono> _worldComponentMonoPool;
        
        private Dictionary<Type, IEcsWorldComponentPoolBase> _pools;

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

        protected virtual void InitPools(in Dictionary<Type, IEcsWorldComponentPoolBase> p)
        {
        }
      
        
        public virtual E CreateEntity()
        {
            return null;
        }

        public virtual void DestroyEntity(E entity)
        {
        }

        
        
        public void RegisterEntity(E entity)
        {
            entity.RegisterInWorld(this);

            _entities.Add(entity);

            OnEntityAddComponents(entity);
        }

        public void UnregisterEntity(E entity)
        {
            _entities.Remove(entity);

            OnEntityDelFromWorld(entity);

            entity.UnregisterFromWorld(this);
        }

        public E GetAnyEntityWith<C>() where C : class, IEcsComponent
        {
            foreach (var entity in _entities)
            {
                if (entity.Has<C>()) return entity;
            }

            return null;
        }
        
        public void OnEntityClearComponents(E entity) //Del all components
        {
            OnEntityDelFromWorld(entity);
        }
        
        public void OnEntityAddComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            if (component != null)
            {
                if (_pools.ContainsKey(typeof(T)))
                {
                    _pools[typeof(T)].Add(component);
                }
            }

            TryRegisterEntityToSystems(entity);
        }

        private void TryRegisterEntityToSystems(E entity)
        {
            for (p = 0; p < _systems.Count; p++)
            {
                _systems[p].TryRegisterEntityComponents(entity); //Пытаемся зарегистрировать эту сущьность в систему
            }
        }

        public void OnEntityAddComponents(E entity)
        {
            foreach (var type in entity.Types)
            {
                if (_pools.ContainsKey(type))
                {
                    _pools[type].Add(entity.Get(type));
                }
            }

            TryRegisterEntityToSystems(entity);
        }

        private void OnEntityDelFromWorld(E entity)
        {
            foreach (var type in entity.Types)
            {
                if (_pools.ContainsKey(type))
                {
                    _pools[type].Remove(entity.Get(type));
                }
            }

            SilentUnregisterEntityFromSystems(entity);
        }

        public void OnEntityDelComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
          
            if (component != null)
            {
                if (_pools.ContainsKey(typeof(T)))
                {
                    _pools[typeof(T)].Remove(component);
                }
            }

            TryUnregisterEntityFromSystems(entity);
        }

        private void SilentUnregisterEntityFromSystems(E entity)
        {
            for (l = 0; l < _systems.Count; l++)
            {
                _systems[l].SilentUnregisterEntity(entity); //Пытаемся удалить сущьность из системы
            }
        }
        
        private void TryUnregisterEntityFromSystems(E entity)
        {
            for (l = 0; l < _systems.Count; l++)
            {
                _systems[l].TryUnregisterEntityComponents(entity); //Пытаемся удалить сущьность из системы
            }
        }

        public void InitWorld(IEcsCore core)
        {
            _core = core;

            _worldComponentPool = new WorldComponentPool<E, EcsComponent>(this);
            _worldComponentMonoPool = new WorldComponentPool<E, EcsComponentMono>(this);
            
            Init();
        }

        private GameObject _systemsTransform;

        private void Init()
        {
            var systemsGo = new GameObject("Systems");
            systemsGo.transform.parent = this.transform;
            _systemsTransform = systemsGo;

            _systems = new List<IEcsSystem<E>>();
            _updateSystems = new List<IEcsSystem<E>>();
            _fixedSystems = new List<IEcsSystem<E>>();
            _lateSystems = new List<IEcsSystem<E>>();

            InitSystems();

            _systems.AddRange(_updateSystems);
            _systems.AddRange(_fixedSystems);
            _systems.AddRange(_lateSystems);

            foreach (var system in _systems)
            {
                system.InitFromWorld(this, _core);
            }

            _pools = new Dictionary<Type, IEcsWorldComponentPoolBase>();
            InitPools(_pools);
        }

        protected abstract void InitSystems();

        protected void CreateUpdateSystem<T>() where T : EcsSystemAbstract<E> =>
            CreateSystem<T>(_updateSystems, _systemsTransform);

        protected void CreateFixedUpdateSystem<T>() where T : EcsSystemAbstract<E> =>
            CreateSystem<T>(_fixedSystems, _systemsTransform);

        protected void CreateLateUpdateSystem<T>() where T : EcsSystemAbstract<E> =>
            CreateSystem<T>(_lateSystems, _systemsTransform);

        private void CreateSystem<T>(List<IEcsSystem<E>> list, GameObject parent) where T : EcsSystemAbstract<E>
        {
            if (parent.TryGetComponent(out T system))
            {
                list.Add(system);

                return;
            }

            list.Add(parent.AddComponent<T>());
        }

        private int i, j, k, l, p;

        public void Update()
        {
            for (i = 0; i < _updateSystems.Count; i++)
            {
                _updateSystems[i].UpdateSys();
            }
        }

        public void FixedUpdate()
        {
            for (j = 0; j < _fixedSystems.Count; j++)
            {
                _fixedSystems[j].FixedUpdateSys();
            }
        }

        public void LateUpdate()
        {
            for (k = 0; k < _lateSystems.Count; k++)
            {
                _lateSystems[k].LateUpdateSys();
            }
        }

        public void Destruct()
        {
            foreach (var system in _systems)
            {
                system.DestructFromWorld(this);
            }
        }
    }
}