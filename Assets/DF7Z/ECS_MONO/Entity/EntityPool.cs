using System;
using System.Collections.Concurrent;

namespace ECS_MONO
{
    internal static class EntityPool
    {
        private static ConcurrentBag<Entity> _objects;
        private static Func<Entity> _objectGenerator;
        
        static EntityPool()
        {
            _objects = new ConcurrentBag<Entity>();
        }

        public static void InitGenerator(Func<Entity> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
        }

        public static Entity Get()
        {
            if (_objects.TryTake(out Entity item))
            {
                return item;
            }
           
            return _objectGenerator();
        }

        public static void Return(Entity item) => _objects.Add(item);
        
    }
}