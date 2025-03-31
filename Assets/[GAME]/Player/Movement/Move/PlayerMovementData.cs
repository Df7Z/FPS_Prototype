using UnityEngine;

namespace Game.Player.Move
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Game/Player/Move SD")]
    internal class PlayerMovementData : ScriptableObject
    {
        public bool _enableBHOP = false;
        
        public CmdMove _groundMove;
        public CmdMove _airMove;
        
        [Min(0f)] public float _sens = 5f;
        [Min(0f)]public float _jumpSpeed = 10f;
        
        public bool _holdJumpToBhop = false;
        [Min(0f)] public float _gravity = 20.0f ;
        [Min(0f)] public float _friction = 6;
        [Min(0f)] public float _airControl = 1f;
        
        public bool EnableBhop => _enableBHOP;

        public CmdMove GroundMove => _groundMove;

        public CmdMove AirMove => _airMove;

        public float Sens => _sens;

        public float JumpSpeed => _jumpSpeed;

        public bool HoldJumpToBhop => _holdJumpToBhop;

        public float Gravity => _gravity;

        public float Friction => _friction;

        public float AirControl => _airControl;
    }
}