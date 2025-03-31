using System;
using System.Collections.Generic;

namespace ECS_MONO
{
    public abstract class EcsSystemBase<T, E> : EcsSystemAbstract<E> where T : EcsNetBaseSystemComponents<E>, new() where E : class, IEntity
    {
        private IEcsCore _core;

        protected EcsWorldAbstract<E> GetWorld(WorldId id) => (EcsWorldAbstract<E>) EcsCore.I.GetWorld<E>(id);
        
        private ObjectPool<T> _pool;
       
        private HashSet<T> _components;
        
        private HashSet<T> _toDelete; //После того как система завершит работу
        private HashSet<T> _toAdd; //После того как система завершит работу

        private bool _isUpdate; //Hash используется потоком

        protected override void Init(IEcsCore core)
        {
            _core = core;
            
            EcsCore.OnInitialized += OnCoreInitialized;
            
            _toDelete = new HashSet<T>(DefaultComponentsDelCapacity);
            _toAdd = new HashSet<T>(DefaultComponentsAddCapacity);
            
            _pool = new ObjectPool<T>(() => new T() );

            InitRequiredTypesForSystem(out _requiredTypes);
            
            _components = new HashSet<T>(DefaultInitComponentsCapacity);
        }

        private void OnCoreInitializedComplete()
        {
            OnCoreInitialized();
            
            EcsCore.OnInitialized -= OnCoreInitialized;
        }

        protected override void OnDestruct()
        {
            base.OnDestruct();
            
            EcsCore.OnInitialized -= OnCoreInitialized;
        }

        protected virtual void OnCoreInitialized() {}

        protected abstract void InitRequiredTypesForSystem(out Type[] types);
        
        private T GetEntityComponents(E entity)
        {
            foreach (var component in _components)
            {
                if (component.Entity == entity) return component;
            }

            return null;
        }
        
        public override bool TryRegisterEntityComponents(E entity)
        {
            if (!CanRegisterEntity(entity)) return false; //Компонент отсутсвует
 
            if (GetEntityComponents(entity) != null) return false; //Сущьность уже используется системой
            
            var newComponents = _pool.Get();
            
            newComponents.Entity = entity;
            
            InitNewComponentField(in entity, in newComponents);
           
            if (!_isUpdate)
            {
                _components.Add(newComponents);

                return true;
            }
            
            _toAdd.Add(newComponents);

            return true;
        }

        public override bool SilentUnregisterEntityComponents(E entity)
        {
            foreach (var component in _components)
            {
                if (component.Entity == entity)
                {
                    if (!_isUpdate)
                    {
                        _pool.Return(component);
                        _components.Remove(component);

                        return true;
                    }
                    
                    _pool.Return(component);
                    _toDelete.Add(component);
                    
                    return true;
                }
            }
            
            //Сущьность отсутсвует в системе
            return false;
        }

        public override void SilentUnregisterEntity(E entity)
        {
            foreach (var component in _components)
            {
                if (component.Entity == entity)
                {
                    if (!_isUpdate)
                    {
                        _pool.Return(component);
                        _components.Remove(component);

                        return;
                    }
                    
                    _pool.Return(component);
                    _toDelete.Add(component);
                    
                    return;
                }
            }
        }

        public override bool TryUnregisterEntityComponents(E entity)
        {
            if (!CanUnregisterEntity(entity)) return false; //Компоненты не удалёны

            return SilentUnregisterEntityComponents(entity);
        }
        
        private void HandleOperations() //Обновление если удален или добавлен компонент для системы
        {
            //add
            if (_toAdd.Count > 0)
            {
                foreach (var components in _toAdd)
                {
                    _components.Add(components);
                }

                _toAdd.Clear();
            }
            //Del
            if (_toDelete.Count > 0)
            {
                foreach (var components in _toDelete)
                {
                    _components.Remove(components);
                }

                _toDelete.Clear();
            }
        }
        
        internal override void UpdateSys()
        {
            _isUpdate = true;
            
            foreach (var component in _components)
            {
                UpdateSystem(component);
            }

            _isUpdate = false;
            
            HandleOperations();
        }
        
        internal override void FixedUpdateSys()
        {
            _isUpdate = true;
            
            foreach (var component in _components)
            {
                FixedUpdateSystem(component);
            }

            _isUpdate = false;
            
            HandleOperations();
        }
        
        internal override void LateUpdateSys()
        {
            _isUpdate = true;
            
            foreach (var component in _components)
            {
                LateUpdateSystem(component);
            }
            
            _isUpdate = false;
            
            HandleOperations(); 
        }


        protected internal abstract void InitNewComponentField(in E entity, in T components);
        protected internal abstract void UpdateSystem(T components);
        protected internal abstract void LateUpdateSystem(T components);
        protected internal abstract void FixedUpdateSystem(T components);
    }
}