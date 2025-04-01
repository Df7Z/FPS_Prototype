using ECS_MONO;
using Game.Player.UI;

namespace Game.Player.Dead
{
    internal sealed class PlayerFinishLevelSystem : EcsSystemMono<PlayerScreens, FinishLevelSignal>
    {
        protected override void Run(EntityMono e, PlayerScreens screens, FinishLevelSignal c2)
        {
            if (screens.FinishScreen.IsActive) return;
            
            screens.FinishScreen.SetView(true);
            
            e.Del<FinishLevelSignal>();
        }
    }
}