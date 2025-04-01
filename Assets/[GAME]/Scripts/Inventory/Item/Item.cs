using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    [RequireComponent(typeof(ItemView))]
    internal sealed class Item : EcsComponentMono
    {
        [SerializeField] private ItemData _itemData;

        public ItemData ItemData => _itemData;

        private void OnValidate()
        {
            if (gameObject.layer != LayerMask.NameToLayer("Item"))
            {
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
        }
    }
}