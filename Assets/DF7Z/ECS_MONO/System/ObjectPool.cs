﻿using System;
using System.Collections.Concurrent;

namespace ECS_MONO
{
    internal class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }

        public T Get()
        {
            if (_objects.TryTake(out T item))
            {
                return item;
            }
            else
            {
                return _objectGenerator();
            }
        }

        public void Return(T item) => _objects.Add(item);
        
    }
}