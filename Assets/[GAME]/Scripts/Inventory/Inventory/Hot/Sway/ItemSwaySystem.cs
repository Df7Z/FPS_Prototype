using ECS_MONO;
using Game.Player;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemSwaySystem : EcsSystemMono<Slot, HotSlot, SelectedHot>
    {
        private PlayerInput _playerInput;
        private IEcsWorldComponentPool<PlayerInput> _pool;

        private Transform _swayTransform;
        private Transform _swayCameraTransform;
        
        private Quaternion rotation;
        private Quaternion _rotationX;
        private Quaternion _rotationY;
        private Quaternion _defaultRotation;
        private Quaternion _target;

        private SwaySettings _currentSwaySettings;

        private float _angle;

        protected override void Run(EntityMono e, Slot slot, HotSlot hotSlot, SelectedHot selected)
        {
            if (_playerInput == null) return;
            if (slot.IsEmpty) return;
            if (!hotSlot.HasView) return;
            if (!hotSlot.HotView.Owner.Has<ItemSway>()) return;

            var sway = hotSlot.HotView.Owner.Get<ItemSway>();

            _swayTransform = sway.Sway;
            _defaultRotation = sway.DefaultSway;
            _currentSwaySettings = sway.Settings;

            DoSway();
        }
        
        private void DoSway()
        {
            rotation = Quaternion.Euler(
                -_playerInput.Axis.y * _currentSwaySettings.MultiplierY * 1f + _target.eulerAngles.x, //sens
                _playerInput.Axis.x * _currentSwaySettings.MultiplierX * 1f + _target.eulerAngles.y,
                0);

            _swayTransform.localRotation = Quaternion.Lerp(_swayTransform.localRotation, rotation,
                _currentSwaySettings.Smooth * Time.deltaTime);

            _rotationX = Quaternion.AngleAxis(-_playerInput.AxisRaw.y * _currentSwaySettings.OffsetMultiplierY,
                Vector3.right);
            _rotationY = Quaternion.AngleAxis(_playerInput.AxisRaw.x * _currentSwaySettings.OffsetMultiplierX,
                Vector3.forward);

            _swayTransform.localRotation = Quaternion.Lerp(_swayTransform.localRotation, _rotationX * _rotationY,
                _currentSwaySettings.OffsetSmooth * Time.deltaTime);

            _angle = Quaternion.Angle(_defaultRotation, _swayTransform.localRotation);

            if (_angle > _currentSwaySettings.MaxAngle)
            {
                _swayTransform.localRotation = Quaternion.Slerp(_defaultRotation, _swayTransform.localRotation,
                    _currentSwaySettings.MaxAngle / _angle);
            }
        }

        protected override void OnCoreInitialized()
        {
            var world = GetWorld(WorldId.Player);

            _pool = world.GetPool<PlayerInput>();

            _pool.OnAdd += OnAddPlayerInput;
            _pool.OnRemove += OnRemovePlayerInput;

            var input = world.GetAnyEntityWith<PlayerInput>();
            if (input != null)
            {
                _playerInput = input.Get<PlayerInput>();
            }
        }

        protected override void OnDestruct()
        {
            _pool.OnAdd -= OnAddPlayerInput;
            _pool.OnRemove -= OnRemovePlayerInput;
        }

        private void OnRemovePlayerInput(PlayerInput component) => _playerInput = null;
        private void OnAddPlayerInput(PlayerInput component) => _playerInput = component;
    }
}