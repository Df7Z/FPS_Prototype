using ECS_MONO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    internal sealed class HotSlot : EcsComponentMono
    {
        public override uint Order => 100;

        [SerializeField] private Image _active;
        
        private ItemHotSlotView _hotView;

        public ItemHotSlotView HotView => _hotView;
        public bool HasView => _hotView != null;

        public void SetView(ItemHotSlotView value) => _hotView = value;

        public void SetActive(bool state) => _active.enabled = state;

        protected override void OnRegisterEntity(IEntity entity)
        {
            if (!entity.Has<SelectedHot>()) SetActive(false);
        }
    }
}