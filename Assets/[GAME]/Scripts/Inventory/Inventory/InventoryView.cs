using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class InventoryView : EcsComponentMono
    {
        public override uint Order => 2000;

        [SerializeField] private Transform _viewTransform;
        
        public void SetView(bool state)
        {
            _viewTransform.gameObject.SetActive(state);
        }
        
        protected override void OnRegisterEntity(IEntity entity)
        {
            SetView(false);
        }

        protected override void OnDespawnPool()
        {
            SetView(false);
        }
    }
}