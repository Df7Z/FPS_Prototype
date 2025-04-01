using ECS_MONO;
using Game.Damage;

namespace Game.Inventory.HealthPack
{
    internal sealed class HealthPackInteractSystem : EcsSystemMono<HealthPack, Item, InteractItemSignal>
    {
        protected override void Run(EntityMono e, HealthPack healthPack, Item item, InteractItemSignal signal)
        {
            TryInteract(e, healthPack, signal.Slot);
            
            e.Del<InteractItemSignal>();
        }

        private void TryInteract(EntityMono e, HealthPack healthPack, Slot slot)
        {
            var inventoryOwner = slot.Collector.OwnerInventory.Entity;

            if (!inventoryOwner.Has<Damaged>()) return;

            var damaged = inventoryOwner.Get<Damaged>();
            var runtime = inventoryOwner.Get<DamagedRuntime>();
            
            if (!healthPack.Heal.Bonus && runtime.Current >= damaged.EnduranceDefault) return;
            
            healthPack.Heal.CreateTransaction(damaged, e);
            
            slot.Clear();
        }
    }
}