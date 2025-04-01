namespace Game.Mobs.Shared
{
    public sealed class MobStayEvent : MobEvent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}