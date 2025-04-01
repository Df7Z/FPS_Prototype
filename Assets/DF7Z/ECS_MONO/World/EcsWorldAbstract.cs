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
        
        private SystemController<E> _systemController;
        private PoolController<E> _poolController;
        
        public T GetFirstWorldComponent<T>() where T : class, IEcsComponent => _poolController.GetFirstWorldComponent<T>();
        
        public IEcsWorldComponentPool<T> GetPool<T>() where T : class, IEcsComponent => _poolController.GetPool<T>();
        
        protected virtual void InitPools(in Dictionary<Type, IEcsWorldComponentPoolBase> p) { }
        public virtual E CreateEntity() => null;
        public virtual void DestroyEntity(E entity) { }

      
        public void RegisterEntity(E entity)
        {
            entity.RegisterInWorld(this);

            _entities.Add(entity);

            OnEntityAddToWorld(entity);
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
        
        internal void OnEntityClearComponents(E entity) => OnEntityDelFromWorld(entity);
        
        internal void OnEntityAddComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            _poolController.EntityAddComponent(entity, component);
            _systemController.EntityAddComponent(entity, component);
        }
        
        internal void OnEntityAddToWorld(E entity)
        {
            _poolController.EntityAddToWorld(entity);    
            _systemController.EntityAddToWorld(entity);
        }

        private void OnEntityDelFromWorld(E entity)
        {
            _poolController.EntityDelFromWorld(entity);    
            _systemController.EntityDelFromWorld(entity);
        }

        internal void OnEntityDelComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            _poolController.EntityDelComponent(entity, component);
            _systemController.EntityDelComponent(entity, component);
        }
        
        public void InitWorld(IEcsCore core)
        {
            _core = core;
            Init();
        }
        
        private void Init()
        {
            var systemsGo = new GameObject($"{ID}Systems");
            systemsGo.transform.parent = this.transform;
            
            _systemController = new SystemController<E>(systemsGo);
            
            InitSystems();

            _systemController.InitSystemsFromWorld(this, _core);
            
            var pools = new Dictionary<Type, IEcsWorldComponentPoolBase>();
            InitPools(pools);
            _poolController = new PoolController<E>(pools);

            EcsCore.OnInitialized += OnCoreInitialize;
        }

        private void OnCoreInitialize()
        {
            _systemController.OnCoreInitialize();
        }

        private void OnDestroy()
        {
            EcsCore.OnInitialized -= OnCoreInitialize;
        }

        #region SYSTEMS
        
        
        protected abstract void InitSystems();
        protected void CreateUpdateSystem<T>() where T : EcsSystemAbstract<E> => _systemController.CreateUpdateSystem<T>();
        protected void CreateFixedUpdateSystem<T>() where T : EcsSystemAbstract<E> => _systemController.CreateFixedUpdateSystem<T>();
        protected void CreateLateUpdateSystem<T>() where T : EcsSystemAbstract<E> => _systemController.CreateLateUpdateSystem<T>();
        
        public void Update() => _systemController.Update();
        public void FixedUpdate() => _systemController.FixedUpdate();
        public void LateUpdate() => _systemController.LateUpdate();
        
        public void Destruct()
        {
            _systemController.Destruct(this);
        }
        
        #endregion
    }
}