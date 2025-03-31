using ECS_MONO;
using TMPro;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class SlotCountView : EcsComponentMono
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text) => _text.text = text;

        protected override void OnRegisterEntity(IEntity entity)
        {
            SetText(string.Empty);
        }
    }
}