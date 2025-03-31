namespace Game.Inventory
{
    internal sealed class SecondaryInteractSignal : InteractSignal
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}