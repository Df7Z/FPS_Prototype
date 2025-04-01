using ECS_MONO;

namespace Game.Player
{
    public sealed class PlayerTag : EcsComponent
    {
        public override uint Order => 99999999;

        protected override void OnRegisterEntity(IEntity entity)
        {
            Owner.Add<PlayerSpawnSignal>();
        }
    }
}