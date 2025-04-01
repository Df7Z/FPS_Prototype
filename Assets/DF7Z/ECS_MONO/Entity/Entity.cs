using System;
using System.Collections.Generic;

namespace ECS_MONO
{
    public class Entity : IEntity
    {
        private const int _initSizeCollection = 4;
        
        private HashSet<IEcsComponent> _components;
        private HashSet<Type> _types;
        
        private EcsWorldAbstract<Entity> _entityWorld;
        
        public Entity()
        {
            _components = new HashSet<IEcsComponent>(_initSizeCollection);
            _types = new HashSet<Type>(_initSizeCollection);
        }
        
        public bool Has<C>() where C : class, IEcsComponent
        {
            return _types.Contains(typeof(C));
        }

        public C Get<C>() where C : class, IEcsComponent
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(C)) return (C) component;
            }

            throw new Exception($"Entity not has {typeof(C)} component!");
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

        public bool TryGet<C>(out C component) where C : class, IEcsComponent
        {
            component = null;
            
            if (!Has<C>()) return false;

            component = Get<C>();
            
            return true;
        }
        
        public void Clear()
        {
            foreach (var component in _components)
            {
                component.UnregisterEntity(this);

                if (component.ComponentType == ComponentType.Default)
                {
                    ComponentPool.ReturnNoNew(component);
                }
            }
            
            _entityWorld.OnEntityClearComponents(this);
            
            _components.Clear();
            _types.Clear();
        }

        public void RegisterInWorld<E>(IEcsWorld<E> world) where E : class, IEntity
        {
            if (EcsGlobalSetup.IsDebug)
            {
                if (_entityWorld != null) throw new Exception("Entity world != null! Maybe new world don't have ID!");
            }
            
            _entityWorld = (EcsWorldAbstract<Entity>) world;
        }

        public void UnregisterFromWorld<E>(IEcsWorld<E> world) where E : class, IEntity
        {
            if (EcsGlobalSetup.IsDebug)
            {
                if (_entityWorld == null) throw new Exception("Entity world is null!");
            }
            
            _entityWorld = null;
        }

        public HashSet<IEcsComponent> Components => _components;
        public HashSet<Type> Types => _types;

        public void AdapterAdd<C>(C component) where C : class, IEcsComponent
        {
            Add(component);

            component.OnOwnerEntitySpawnPool(this);
        }

        public C Add<C>() where C : class, IEcsComponent, new()
        {
            return Add(ComponentPool.Give<C>());
        }
        
        public C SafeAdd<C>() where C : class, IEcsComponent, new()
        {
            if (Has<C>()) return Get<C>();
            
            return Add(ComponentPool.Give<C>());
        }
        
        public C Add<C>(C component) where C : class, IEcsComponent
        {
            component.RegisterEntity(this);
          
            _components.Add(component);
            _types.Add(typeof(C));
            
            _entityWorld.OnEntityAddComponent<C>(this, component);
            
            return component;
        }
        
        public void Del<C>(C component) where C : class, IEcsComponent, new()
        {
            DeleteComplete<C>(component);
        }

        public void SafeDel<C>() where C : class, IEcsComponent, new()
        {
            if (!Has<C>()) return;

            Del<C>();
        }

      

        public void Del<C>() where C : class, IEcsComponent, new()
        {
            foreach (var element in _components)
            {
                if (element.GetType() == typeof(C))
                {
                    var component = (C) element;
                    
                    DeleteComplete<C>(component);
                    
                    return;
                }
            }
            
            throw new Exception($"Entity not can del {typeof(C)} component!");
        }
        
        public void SilentDel<C>(C component) where C : class, IEcsComponent, new()
        {
            //world == null
            component.UnregisterEntity(this);
            
            _types.Remove(typeof(C));
            _components.Remove(component);
            _entityWorld?.OnEntityDelComponent<C>(this, component);
            if (component.ComponentType == ComponentType.Default)
            {
                ComponentPool.Return(component);
            }
        }
        
        private void DeleteComplete<C>(C component)  where C: class, IEcsComponent, new()
        {
            component.UnregisterEntity(this);
            
            _types.Remove(typeof(C));
            _components.Remove(component);
            
            _entityWorld.OnEntityDelComponent<C>(this, component);
            
            if (component.ComponentType == ComponentType.Default)
            {
                ComponentPool.Return(component);
            }

            
        }
    }
}