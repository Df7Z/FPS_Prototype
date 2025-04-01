namespace Game.AI
{
    internal sealed class AITaskChase : AITask
    {
        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}