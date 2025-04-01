namespace Game.Inventory
{
    internal sealed class SecondaryInteractSignal : InteractSignal
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}