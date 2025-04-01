namespace Game.Inventory
{
    internal sealed class SecondaryInteractStopSignal : InteractSignal
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}