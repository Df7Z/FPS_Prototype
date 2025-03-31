using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    [RequireComponent(typeof(WeaponReloadRuntime))]
    internal sealed class WeaponReload : EcsComponentMono
    {
        [SerializeField] private int _default = 10;
        [SerializeField] private int _max = 10;
        [SerializeField] private float _time = 1f;
        [SerializeField] private ItemId _ammoType = ItemId.ClipAmmo;
        [SerializeField] private EntityAnimation _animation;
        public EntityAnimation Animation => _animation;
        public ItemId AmmoType => _ammoType;
        public int Max => _max;
        public int Min => 1;
        public float Time => _time;
        public int DefaultCount => _default;

        private void Awake()
        {
            _animation.MakeHash();
        }
        
        
    }
}