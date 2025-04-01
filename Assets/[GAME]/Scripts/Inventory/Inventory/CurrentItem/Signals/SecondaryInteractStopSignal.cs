namespace Game.Inventory
{
    internal sealed class SecondaryInteractStopSignal : InteractSignal
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}