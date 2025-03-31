using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_MONO
{
    public interface IEcsWorldComponentPoolBase
    {
        event Action<IEcsComponent> OnAdd;
        event Action<IEcsComponent> OnRemove;

        public void Add(IEcsComponent component);
        public void Remove(IEcsComponent component);

        public IEcsComponent GetFirstComponent();
    }

    public interface IEcsWorldComponentPool<T> : IEcsWorldComponentPoolBase where T : class, IEcsComponent
    {
        new event Action<T> OnAdd;
        new event Action<T> OnRemove;
    }

    public sealed class EcsWorldComponentPool<T> : IEcsWorldComponentPool<T> where T : class, IEcsComponent
    {
        private HashSet<T> _components = new HashSet<T>();

        public IEcsComponent GetFirstComponent()
        {
            return _components.First();
        }
        
        public void Add(IEcsComponent component)
        {
            var typed = (T) component;
            
            _components.Add(typed);
            
            onAdd?.Invoke(typed);
        }

        public void Remove(IEcsComponent component)
        { 
            var typed = (T) component;
            
            _components.Remove(typed);
            
            onRemove?.Invoke(typed);
        }

       


        private event Action<T> onAdd;
        private event Action<T> onRemove;

        public event Action<T> OnAdd
        {
            add => onAdd += value;
            remove => onAdd -= value;
        }

        public event Action<T> OnRemove
        {
            add => onRemove += value;
            remove => onRemove -= value;
        }
        
        event Action<IEcsComponent> IEcsWorldComponentPoolBase.OnAdd
        {
            add => onAdd += value as Action<T>;
            remove => onAdd -= value as Action<T>;
        }

        event Action<IEcsComponent> IEcsWorldComponentPoolBase.OnRemove
        {
            add => onRemove += value as Action<T>;
            remove => onRemove -= value as Action<T>;
        }

      
        
    }
}