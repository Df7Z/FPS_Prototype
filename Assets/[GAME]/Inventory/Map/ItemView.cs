using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemView : EcsComponentMono
    {
        [SerializeField] private Transform _viewTransform;

        public Transform ViewTransform => _viewTransform;

        public override void OnSpawnPool(IEntity entity)
        {
            ViewTransform.gameObject.SetActive(true);
        }
    }
}