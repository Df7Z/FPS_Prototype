using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game/Item/SD")]
    internal sealed class ItemData : ScriptableObject
    {
        [SerializeField] private ItemId _id;
        [SerializeField] private Sprite _icon;
        
        public ItemId ID => _id;
        public Sprite Icon => _icon;
    }
}