namespace Game.Inventory
{
    internal sealed class ReloadInteractSignal : InteractSignal
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}