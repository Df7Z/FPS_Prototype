using System.Collections.Generic;
using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    public sealed class PlayerInput : EcsComponentMono
    {
        private Vector2 _axis; //Mouse
        private Vector2 _axisRaw; //Dir
        private Vector2 _axisRawNormalized;
        private bool _isJump;
        private bool _isJumpUp;
        private bool _isJumpDown;
        private bool _isCrouch;
        private bool _isInventoryDown;
        private float _mouseWheel;
        private readonly bool[] _numDowns = new bool[10];
        private bool _isLeftMouseButton;
        private bool _isRightMouseButton;
        private bool _isRightMouseButtonDown;
        private bool _isRightMouseButtonUp;
        private bool _isReload;
        
        public bool IsLeftMouseButton => _isLeftMouseButton;
        public bool IsRightMouseButton => _isRightMouseButton;
        public bool IsRightMouseButtonDown => _isRightMouseButtonDown;
        public bool IsRightMouseButtonUp => _isRightMouseButtonUp;
        public bool IsReload => _isReload;
        public float MouseWheel => _mouseWheel;
        public bool[] NumDowns => _numDowns;
        public bool IsInventoryDown => _isInventoryDown;
        public bool IsJump => _isJump;
        public bool IsJumpUp => _isJumpUp;
        public bool IsJumpDown => _isJumpDown;
        public bool IsCrouch => _isCrouch;
        public Vector2 Axis => _axis;
        public Vector2 AxisRaw => _axisRaw;
        public Vector2 AxisRawNormalized => _axisRawNormalized;

        public void SetInventoryDown(bool value) => _isInventoryDown = value;
        public void SetJump(bool value) => _isJump = value;
        public void SetJumpUp(bool value) => _isJumpUp = value;
        public void SetJumpDown(bool value) => _isJumpDown = value;
        public void SetIsLeftMouseButton(bool value) => _isLeftMouseButton = value;
        public void SetIsRightMouseButton(bool value) => _isRightMouseButton = value;
        public void SetIsRightMouseButtonDown(bool value) => _isRightMouseButtonDown = value;
        public void SetIsRightMouseButtonUp(bool value) => _isRightMouseButtonUp = value;
        public void SetIsReload(bool value) => _isReload = value;
        
        public void SetAxis(float x, float y)
        {
            _axis.x = x;
            _axis.y = y;
        }

        public void SetAxisRawNormalized(Vector2 value) => _axisRawNormalized = value;
        
        public void SetAxisRaw(float x, float y)
        {
            _axisRaw.x = x;
            _axisRaw.y = y;
        }

        public void SetMouseWheel(float value) => _mouseWheel = value;
        public void SetNumDown(int index, bool value) => _numDowns[index] = value;

        public void SetNumDowns(bool value = false)
        {
            for (int i = 0; i < _numDowns.Length; i++)
            {
                _numDowns[i] = value;
            }
        }
        
        public void SetCrouch(bool value)=> _isCrouch = value;
    }
}