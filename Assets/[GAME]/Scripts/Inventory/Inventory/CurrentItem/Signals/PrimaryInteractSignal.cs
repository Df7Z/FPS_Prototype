namespace Game.Inventory
{
    internal sealed class PrimaryInteractSignal : InteractSignal
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}