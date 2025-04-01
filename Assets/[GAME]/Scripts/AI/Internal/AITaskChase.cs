namespace Game.AI
{
    internal sealed class AITaskChase : AITask
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}