namespace Game.Inventory
{
    internal sealed class SecondaryInteractStartSignal : InteractSignal
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}