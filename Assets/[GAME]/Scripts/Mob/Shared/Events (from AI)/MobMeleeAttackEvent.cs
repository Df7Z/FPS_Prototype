namespace Game.Mobs.Shared
{
    public sealed class MobMeleeAttackEvent : MobEvent
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}