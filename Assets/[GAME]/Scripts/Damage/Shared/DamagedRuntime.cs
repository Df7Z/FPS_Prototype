using System;
using ECS_MONO;

namespace Game.Damage
{
    public sealed class DamagedDead : EcsComponent
    {
        
    }

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

                Dead();
                
                return;
            }
            
            _current -= value;

            UpdateUI();
        }

        private void Dead()
        {
            Owner.Add<DamagedDead>();
        }
        
        private void UpdateUI()
        {
            if (Owner.Has<DamagedUIView>())
            {
                Owner.Get<DamagedUIView>().SetCountText(_current.ToString());
            }
            
            if (Owner.Has<Damaged3DUIView>())
            {
                Owner.Get<Damaged3DUIView>().SetCountText(_current.ToString());
            }
        }
    }
}