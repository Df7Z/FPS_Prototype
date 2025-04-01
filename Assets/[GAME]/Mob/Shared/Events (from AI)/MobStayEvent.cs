namespace Game.Mobs.Shared
{
    public sealed class MobStayEvent : MobEvent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}