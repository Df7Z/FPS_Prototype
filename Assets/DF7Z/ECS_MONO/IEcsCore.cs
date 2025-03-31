using System;

namespace ECS_MONO
{
    public interface IEcsCore
    {
        public IEcsWorld<T> GetWorld<T>(WorldId id) where T : class, IEntity;
    }
}