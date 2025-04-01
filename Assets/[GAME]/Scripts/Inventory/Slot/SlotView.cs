using ECS_MONO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    internal sealed class SlotView : EcsComponentMono
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _imageIcon;

        protected override void OnRegisterEntity(IEntity entity)
        {
            _button.onClick.AddListener(OnClick);
            
            _imageIcon.enabled = false;
        }

        protected override void OnUnregisterEntity(IEntity entity)
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (!Owner.Has<ClickSlotSignal>())
            {
                Owner.Add<ClickSlotSignal>();
            }
        }

        public void UpdateIcon(Sprite sprite)
        {
            _imageIcon.enabled = true;
            _imageIcon.sprite = sprite;
        }

        public void Clear()
        {
            _imageIcon.enabled = false;
        }
    }
}