using System;
using ECS_MONO;

namespace Game.Damage
{
    public sealed class DamagedRuntime : EcsComponent
    {
        private uint _current;
        public bool IsDead => _current == 0;
        public uint Current => _current;

        public void Set(uint value)
        {
            _current = value;

            UpdateUI();
        }

        public void Add(uint value)
        {
            _current += value;

            UpdateUI();
        }
        
        public void Take(uint value)
        {
            if (value >= _current)
            {
                _current = 0;

                UpdateUI();
                
                return;
            }
            
            _current -= value;

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (Owner.Has<DamagedUIView>())
            {
                Owner.Get<DamagedUIView>().SetCountText(_current.ToString());
            }
        }
    }
}