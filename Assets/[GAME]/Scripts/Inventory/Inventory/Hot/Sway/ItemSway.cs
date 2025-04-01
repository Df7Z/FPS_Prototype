using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemSway : EcsComponentMono
    {
        [SerializeField] private Transform _sway;
        [SerializeField] private SwaySettings _settings;

        private Quaternion _defaultSway;

        public Transform Sway => _sway;
        public Quaternion DefaultSway => _defaultSway;
        public SwaySettings Settings => _settings;

        protected override void OnRegisterEntity(IEntity entity)
        {
            _defaultSway = _sway.localRotation;
        }
    }
}