using ECS_MONO;
using Game.Player;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class InventoryChangeViewSystem : EcsSystemMono<Inventory, InventoryView>
    {
        private PlayerInput _playerInput;
        private IEcsWorldComponentPool<PlayerInput> _pool;

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
        
        protected override void Run(EntityMono e, Inventory inventory, InventoryView view)
        {
            if (_playerInput == null) return;
            if (!_playerInput.IsInventoryDown) return;

            var runtime = inventory.Owner.Get<InventoryRuntime>();
            
            var state = !runtime.IsOpen;
            
            view.SetView(state);

            if (!state)
            {
                if (!_playerInput.Owner.Has<ChangeInputStateSignal>())
                {
                    var signalInput = _playerInput.Owner.Add<ChangeInputStateSignal>();
                    
                    signalInput.Target = PlayerInputState.Default;
                    
                    var signalCursor = _playerInput.Owner.Add<ChangeCursorSignal>();

                    signalCursor.Target = CursorLockMode.Locked;
                }
            }
            else
            {
                if (!_playerInput.Owner.Has<ChangeInputStateSignal>())
                {
                    var signalInput = _playerInput.Owner.Add<ChangeInputStateSignal>();

                    signalInput.Target = PlayerInputState.Inventory;
                    
                    var signalCursor = _playerInput.Owner.Add<ChangeCursorSignal>();

                    signalCursor.Target = CursorLockMode.None;
                }
            }

            runtime.IsOpen = state;
        }
    }
}