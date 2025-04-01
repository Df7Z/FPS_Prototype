namespace Game.Mobs.Shared
{
    public sealed class MobDeathEvent : MobEvent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}