using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class HotItem : EcsComponentMono //Предмет хранится в горячем слоте
    {
        [SerializeField] private ItemHotSlotView _prefabView;

        public ItemHotSlotView PrefabView => _prefabView;
    } 
}