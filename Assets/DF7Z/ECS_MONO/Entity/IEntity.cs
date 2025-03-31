using System;
using System.Collections;
using System.Collections.Generic;
using PoolSystem;

namespace ECS_MONO
{
    public interface IEntity 
    {
        public C Add<C>(C component) where C : class, IEcsComponent;
        public C Add<C>() where C : class, IEcsComponent, new();
        public void SafeDel<C>() where C : class, IEcsComponent, new();
        public C SafeAdd<C>() where C : class, IEcsComponent, new();
        public void Del<C>() where C : class, IEcsComponent, new();
        public void Del<C>(C component) where C : class, IEcsComponent, new();
        public void SilentDel<C>(C component) where C : class, IEcsComponent, new();
        public bool Has<C>() where C : class, IEcsComponent;
        public C Get<C>() where C : class, IEcsComponent;
        public bool TryGet<C>(out C component)  where C : class, IEcsComponent;
        public IEcsComponent Get(Type type);
        
        public void Clear(); //Components
        
        public void RegisterInWorld<E>(IEcsWorld<E> world) where E : class ,IEntity;
        public void UnregisterFromWorld<E>(IEcsWorld<E> world) where E : class ,IEntity;
        public HashSet<IEcsComponent> Components { get; }
        public HashSet<Type> Types { get; }
    }
}