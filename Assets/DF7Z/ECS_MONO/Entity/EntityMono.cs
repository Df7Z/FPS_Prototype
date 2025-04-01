using System;
using System.Collections.Generic;
using System.Linq;
using PoolSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ECS_MONO
{
    public sealed class EntityMono : MonoBehaviour, IEntityMono, IPoolItem
    {
        [SerializeField] private WorldId _world = WorldId.Default;

        private const int _initSizeCollection = 8;
        private HashSet<IEcsComponent> _components;
        private HashSet<Type> _types;
        internal EcsWorldAbstract<EntityMono> _entityWorld;
        private EcsCore _core;
        private bool _inPool;

        public bool InPool => _inPool;
        public void OnSpawn() => SpawnFromPool();
        public void OnDespawn() => DespawnFromPool();
        public HashSet<IEcsComponent> Components => _components;
        public HashSet<Type> Types => _types;
        private void InitCore()
        {
            if (EcsCore.I == null)
            {
                EcsCore.OnInitialized += CoreInitialize;
            }
            else
            {
                CoreInitialize();
            }
        }
        
        private void CoreInitialize()
        {
            _core = EcsCore.I;
            
            InitComponents();
        }

        private void InitComponents()
        {
            var world = _core.GetWorld<EntityMono>(_world);
            
            world.RegisterEntity(this);
            
            var components = GetComponents<IEcsComponent>().OrderBy(component => component.Order);
            foreach (var component in components)
            {
                _components.Add(component);
                _types.Add(component.GetType());
                component.RegisterEntity(this);
                component.OnOwnerEntitySpawnPool(this);   
            }

            var adaptersComponents = GetComponents<IComponentAdapter>().OrderBy(adapter => adapter.Order).ToArray();
            for (int i = 0; i < adaptersComponents.Length; i++)
            {
                var adapter = adaptersComponents[i];
                
                adapter.SetComponent(this);
                
                adapter.SilentDestroy();
            }
            
            _entityWorld.OnEntityAddToWorld(this);
        }

       
        private void Awake()
        {
            _components = new HashSet<IEcsComponent>(_initSizeCollection);
            _types = new HashSet<Type>(_initSizeCollection);
            
            InitCore();
        }

        private void SpawnFromPool() 
        {
            _core.GetWorld<EntityMono>(_world).RegisterEntity(this);
            
            ErrorWorld();
            
            _inPool = false;
            
            __temp.Clear();
           
            foreach (var component in _components)
                __temp.Add(component);
            
            foreach (var component in __temp)
                component.OnOwnerEntitySpawnPool(this); //Добавление, удаление при спавне
        }

        private HashSet<IEcsComponent> __temp = new HashSet<IEcsComponent>();

        private void DespawnFromPool()
        {
            ErrorWorld();
         
            _inPool = true;
            
            __temp.Clear();
           
            foreach (var component in _components)
                __temp.Add(component);

            foreach (var component in __temp)
            {
                component.OnOwnerEntityDespawnPool(this);
            }

            _core.GetWorld<EntityMono>(_world).UnregisterEntity(this);
            
           //_entityWorld.UnregisterEntity(this);
        }

        public void Clear()
        {
            __temp.Clear();
           
            foreach (var component in _components)
                __temp.Add(component);
            
            foreach (var component in __temp)
            {
                component.OnOwnerEntityDespawnPool(this);
                
                component.UnregisterEntity(this);

                if (component.ComponentType == ComponentType.Mono)
                {
                    var netComponent = component as EcsComponentMono;
                
                    Destroy(netComponent);
                }

                if (component.ComponentType == ComponentType.Default)
                {
                    ComponentPool.ReturnNoNew(component);
                }
            }
            
            _entityWorld.OnEntityClearComponents(this);
            
            _components.Clear();
            _types.Clear();
        }

        private void OnDestroy()
        {
            if (_entityWorld != null)
            {
                //Remove entity from world
                _entityWorld.DestroyEntity(this);
            }

            EcsCore.OnInitialized -= CoreInitialize;
        }

        
        public void RegisterInWorld<E>(IEcsWorld<E> world) where E : class, IEntity
        {
            ErrorType();
            
            _entityWorld = (EcsWorldAbstract<EntityMono>) world;
        }

        public void UnregisterFromWorld<E>(IEcsWorld<E> world) where E : class, IEntity
        {
            ErrorWorld();
            ErrorType();
            
            _entityWorld = null;
        }
        
        public bool Has<C>() where C : class, IEcsComponent
        {
            ErrorType();
            
            return _types.Contains(typeof(C));
        }

        public C Get<C>() where C : class, IEcsComponent
        {
            ErrorWorld();
            ErrorType();
            ErrorNoHas<C>();
            
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(C)) return (C) component;
            }

            throw new Exception($"Entity {gameObject.name} not has {typeof(C)} component!");
        }

        public bool TryGet<C>(out C component) where C : class, IEcsComponent
        {
            component = null;
            
            if (!Has<C>()) return false;

            component = Get<C>();
            
            return true;
        }

        public IEcsComponent Get(Type type)
        {
            if (!_types.Contains(type)) return null;

            foreach (var component in _components)
            {
                if (component.GetType() == type) return component;
            }

            throw new Exception($"Entity not has {type} component!");
        }

        public void AdapterAdd<C>(C component) where C : class, IEcsComponent
        {
            Add(component);

            component.OnOwnerEntitySpawnPool(this);
        }

        public C Add<C>() where C : class, IEcsComponent, new()
        {
            return Add<C>(ComponentPool.Give<C>());
        }
        
        public C SafeAdd<C>() where C : class, IEcsComponent, new()
        {
            if (Has<C>()) return Get<C>();
            
            return Add(ComponentPool.Give<C>());
        }
        
        public C Add<C>(C component) where C : class, IEcsComponent
        {
            ErrorWorld();
            ErrorType();
            ErrorHas<C>();
            
            component.RegisterEntity(this);
            
            _components.Add(component);
            _types.Add(typeof(C));
            
            _entityWorld.OnEntityAddComponent<C>(this, component);
            
            return component;
        }
        
        public C AddMono<C>() where C : EcsComponentMono
        {
            ErrorWorld();
            ErrorType();
            ErrorNoHas<C>();
            
            var component = gameObject.AddComponent<C>();

            if ( ((IEcsComponent)component).ComponentType != ComponentType.Mono) throw new Exception("'AddNet' for mono components only!");
            
            return Add(component);
        }

        public void Del<C>(C component) where C : class, IEcsComponent, new()
        {
            ErrorWorld();
            ErrorType();
            ErrorNoHas<C>();
            
            if (!EcsGlobalSetup.IsDebug)
            {
                if (!Has<C>()) return;
            }
            
            DeleteComplete<C>(component);
        }

        public void SafeDel<C>() where C : class, IEcsComponent, new()
        {
            if (!Has<C>()) return;

            Del<C>();
        }
        
        public void Del<C>() where C : class, IEcsComponent, new()
        {
            ErrorWorld();
            ErrorType();
            ErrorNoHas<C>();
          
            if (!EcsGlobalSetup.IsDebug)
            {
                if (!Has<C>()) return;
            }
            
            foreach (var element in _components)
            {
                if (element.GetType() == typeof(C))
                {
                    var component = (C) element;
                    
                    DeleteComplete<C>(component);
                    
                    return;
                }
            }
            
            throw new Exception($"Entity {gameObject.name} not can del {typeof(C)} component!");
        }

        private void ErrorWorld()
        {
            if (!EcsGlobalSetup.IsDebug) return;
            if (_entityWorld == null) throw new Exception("Entity world is null!");
        }
        
        private void ErrorType()
        {
            if (!EcsGlobalSetup.IsDebug) return;
            if (_types.Count != _components.Count) throw new Exception("Types count != Component count!");
        }

        private void ErrorNoHas<C>() where C : class, IEcsComponent
        { 
            if (!EcsGlobalSetup.IsDebug) return;
            if (!Has<C>()) throw new Exception($"Entity {gameObject.name} not has {typeof(C)} component!");
        }
        
        private void ErrorHas<C>() where C : class, IEcsComponent
        { 
            if (!EcsGlobalSetup.IsDebug) return;
            if (Has<C>()) throw new Exception($"Component {typeof(C)} Has exist on entity {this.name} !");
        }
        
        public void SilentDel<C>(C component) where C : class, IEcsComponent, new()
        {
            ErrorType();
            ErrorNoHas<C>();
            
            Remove(component);
            _entityWorld?.OnEntityDelComponent<C>(this, component);
            DestroyMonoComponent(component);
            DestroyComponent(component);
        }
        
        private void DeleteComplete<C>(C component)  where C: class, IEcsComponent, new()
        {
            Remove(component);
            RemoveWorld(component);
            DestroyMonoComponent(component);
            DestroyComponent(component);
        }

        private void RemoveWorld<C>(C component) where C : class, IEcsComponent, new()
        {
            _entityWorld.OnEntityDelComponent<C>(this, component);
        }

        private void Remove<C>(C component) where C : class, IEcsComponent, new()
        {
            component.UnregisterEntity(this);

            _types.Remove(typeof(C));
            _components.Remove(component);
        }
        
        private void DestroyComponent<C>(C component) where C : class, IEcsComponent, new()
        {
            if (component.ComponentType == ComponentType.Default)
            {
                ComponentPool.Return(component);
            }
        }

        private void DestroyMonoComponent<C>(C component) where C : class, IEcsComponent
        {
            if (component.ComponentType == ComponentType.Mono)
            {
                var netComponent = component as EcsComponentMono;
                
                Destroy(netComponent);
            }
        }
    }
}