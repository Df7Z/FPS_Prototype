using ECS_MONO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Player.UI
{
    internal sealed class PlayerDeadScreenClickSignal : EcsComponent {}

    internal sealed class PlayerDeadScreen : PlayerScreen
    {
        [SerializeField] private Button _respawn;
      
        
        protected override void OnRegisterEntity(IEntity entity)
        {
            _respawn.onClick.AddListener(ContinueClick);
            
            SetView(false);
        }

        public override void OnSpawnPool(IEntity entity)
        {
            SetView(false);
        }

        private void ContinueClick()
        {
            if (_reference.Entity.Has<PlayerDeadScreenClickSignal>()) return;
            
            _reference.Entity.Add<PlayerDeadScreenClickSignal>();
        }

        protected override void OnUnregisterEntity(IEntity entity)
        {
            _respawn.onClick.RemoveListener(ContinueClick);
        }
    }
}