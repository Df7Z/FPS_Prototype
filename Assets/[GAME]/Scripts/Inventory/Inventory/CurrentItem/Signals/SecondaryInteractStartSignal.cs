namespace Game.Inventory
{
    internal sealed class SecondaryInteractStartSignal : InteractSignal
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}