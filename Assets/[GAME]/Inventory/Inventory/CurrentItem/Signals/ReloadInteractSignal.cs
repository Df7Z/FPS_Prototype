namespace Game.Inventory
{
    internal sealed class ReloadInteractSignal : InteractSignal
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}