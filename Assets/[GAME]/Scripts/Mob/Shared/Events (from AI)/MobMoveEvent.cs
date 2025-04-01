namespace Game.Mobs.Shared
{
    public sealed class MobMoveEvent : MobEvent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}