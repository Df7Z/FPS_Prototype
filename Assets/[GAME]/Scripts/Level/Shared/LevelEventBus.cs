using System;
using ECS_MONO;

namespace Game.Level.Shared
{
    public static class LevelEventBus
    {
        public static Action<IEntity> OnPlayerRespawnRequest;
        public static Action<LevelRestartParams> OnLevelRestart;
        public static Action OnLevelFinish;
    }
}