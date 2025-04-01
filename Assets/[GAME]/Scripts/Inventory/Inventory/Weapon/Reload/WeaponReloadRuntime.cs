using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponReloadRuntime : EcsComponentMono
    {
        public override uint Order => 100;
        
        [NonSerialized] private int _current;

        public int Current => _current;

        public void SetCurrent(int value)
        {
            _current = value;
            
            UpdateUI(Owner.Get<WeaponReload>());
        }

        protected override void OnRegisterEntity(IEntity entity) => Initialize(entity);

        protected override void OnSpawnPool() => Initialize(Owner);

        private void Initialize(IEntity entity)
        {
            var reload = entity.Get<WeaponReload>();
            
            _current = reload.DefaultCount;

            UpdateUI(reload);
        }
        
        private void UpdateUI(WeaponReload reload)
        {
            if (Owner.TryGet(out WeaponReloadUI component))
            {
                component.SetCurrent($"{_current} / {reload.Max}");
            }
        }
        
        public void Take(int count = 1)
        {
            if (_current - count < 0) throw new Exception("Current < 0!");
            
            _current -= count;

            UpdateUI(Owner.Get<WeaponReload>());
        }
    }
}