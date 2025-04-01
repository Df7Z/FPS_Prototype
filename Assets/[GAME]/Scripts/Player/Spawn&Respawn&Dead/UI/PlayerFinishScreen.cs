using System;
using ECS_MONO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player.UI
{
    internal sealed class PlayerFinishScreenClickSignal : EcsComponent {}
    
    internal sealed class PlayerFinishScreen : PlayerScreen
    {
        [SerializeField] private Button _continue;
       
        
        protected override void OnRegisterEntity(IEntity entity)
        {
            _continue.onClick.AddListener(ContinueClick);
             
            SetView(false);
        }

        protected override void OnSpawnPool()
        {
            SetView(false);
        }

        private void ContinueClick()
        {
            if (_reference.Entity.Has<PlayerFinishScreenClickSignal>()) return;
            
            _reference.Entity.Add<PlayerFinishScreenClickSignal>();
        }

        protected override void OnUnregisterEntity(IEntity entity)
        {
            _continue.onClick.RemoveListener(ContinueClick);
        }
    }
}