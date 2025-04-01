namespace Game.Mobs.Shared
{
    public sealed class MobDeathEvent : MobEvent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}