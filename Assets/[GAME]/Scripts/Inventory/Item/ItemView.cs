using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemView : EcsComponentMono
    {
        [SerializeField] private Transform _viewTransform;

        public void SetView(bool value) => _viewTransform.gameObject.SetActive(value);

        protected override void OnSpawnPool()
        {
            SetView(true);
        }
    }
}