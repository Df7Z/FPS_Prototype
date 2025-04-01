using ECS_MONO;
using Game.Inventory.Shared;

namespace Game.Inventory
{
    internal sealed class ChangeCanCollectSystem : EcsSystemMono<ChangeCanCollectSignal, ItemCollector>
    {
        protected override void LateRun(EntityMono e, ChangeCanCollectSignal signal, ItemCollector collector)
        {
            if (signal.State)
            {
                e.SafeAdd<CanCollectItem>();
            }
            else
            {
                e.SafeDel<CanCollectItem>();
            }
            
            e.Del<ChangeCanCollectSignal>();
        }
    }
}