namespace Game.Mobs.Shared
{
    public sealed class MobMoveEvent : MobEvent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}