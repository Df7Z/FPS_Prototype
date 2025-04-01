using ECS_MONO;
using Game.Player;

namespace Game.Inventory
{
    internal sealed class HandleInteractHotSlotSystem : EcsSystemMono<Slot, HotSlot, SelectedHot>
    {
        private PlayerInput _playerInput;
        private IEcsWorldComponentPool<PlayerInput> _pool;

        protected override void Run(EntityMono e, Slot slot, HotSlot hotSlot, SelectedHot selected)
        {
            if (_playerInput == null) return;

            if (slot.IsEmpty) return;

            if (!slot.Item.Owner.Has<InteractableItem>()) return;

            if (!hotSlot.HasView) return;
            
            if (_playerInput.IsLeftMouseButton) hotSlot.HotView.Owner.SafeAdd<PrimaryInteractSignal>();
            if (_playerInput.IsRightMouseButton) hotSlot.HotView.Owner.SafeAdd<SecondaryInteractSignal>();
            if (_playerInput.IsRightMouseButtonDown) hotSlot.HotView.Owner.SafeAdd<SecondaryInteractStartSignal>();
            if (_playerInput.IsRightMouseButtonUp) hotSlot.HotView.Owner.SafeAdd<SecondaryInteractStopSignal>();
            if (_playerInput.IsReload) hotSlot.HotView.Owner.SafeAdd<ReloadInteractSignal>();
        }

        protected override void OnCoreInitialized()
        {
            var world = GetWorld(WorldId.Player);
            
            _pool = world.GetPool<PlayerInput>();
            
            _pool.OnAdd += OnAddPlayerInput;
            _pool.OnRemove += OnRemovePlayerInput;
            
            var input= world.GetAnyEntityWith<PlayerInput>();
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