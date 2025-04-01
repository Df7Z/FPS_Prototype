namespace Game.Inventory
{
    internal sealed class PrimaryInteractSignal : InteractSignal
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}