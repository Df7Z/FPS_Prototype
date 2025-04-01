namespace Game.Mobs.Shared
{
    public sealed class MobMeleeAttackEvent : MobEvent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}