using System;
using ECS_MONO;
using Game.Projectile.Shared;
using UnityEngine;

namespace Game.Projectile
{
    internal abstract class ProjectileObject<T> : EcsComponentMono where T : ProjectileData
    {
        protected T _data;

        public bool DataEqualType(ProjectileData data) => data.GetType() == typeof(T);
        
        public void Set(T value) => _data = value;
    }
}