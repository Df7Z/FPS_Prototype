using ECS_MONO;
using Game.Level.Shared;

namespace Game.Player.UI
{
    internal sealed class PlayerFinishScreenSystem : EcsSystemMono<PlayerScreens, PlayerFinishScreenClickSignal>
    {
        protected override void Run(EntityMono e, PlayerScreens screens, PlayerFinishScreenClickSignal c2)
        {
            e.Del<PlayerFinishScreenClickSignal>();
            
            screens.FinishScreen.SetView(false);
            
            LevelEventBus.OnLevelFinish?.Invoke();
        }
    }
}