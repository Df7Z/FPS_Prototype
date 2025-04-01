using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Projectile.Shared
{
    public abstract class ProjectileData : ScriptableObject
    {
        [SerializeField] protected EntityMono _prefab;

        public EntityMono Prefab => _prefab;
    }
}