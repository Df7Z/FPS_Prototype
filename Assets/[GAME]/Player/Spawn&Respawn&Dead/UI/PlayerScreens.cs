using ECS_MONO;
using UnityEngine;

namespace Game.Player.UI
{
    internal class PlayerScreens : EcsComponentMono
    {
        [SerializeField] private PlayerDeadScreen _deadScreen;
        [SerializeField] private PlayerFinishScreen _finishScreen;
        
        public PlayerDeadScreen DeadScreen => _deadScreen;
        public PlayerFinishScreen FinishScreen => _finishScreen;
    }
}