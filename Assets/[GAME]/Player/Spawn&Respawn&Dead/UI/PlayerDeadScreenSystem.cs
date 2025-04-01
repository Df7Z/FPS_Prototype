using ECS_MONO;
using Game.Level.Shared;

namespace Game.Player.UI
{
    internal sealed class PlayerDeadScreenSystem : EcsSystemMono<PlayerScreens, PlayerDeadScreenClickSignal>
    {
        protected override void Run(EntityMono e, PlayerScreens screens, PlayerDeadScreenClickSignal c2)
        {
            e.Del<PlayerDeadScreenClickSignal>();
            
            LevelEventBus.OnPlayerRespawnRequest?.Invoke(e);
            
            screens.DeadScreen.SetView(false);
        }
    }
}