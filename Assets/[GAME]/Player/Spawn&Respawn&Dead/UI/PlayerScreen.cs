using ECS_MONO;
using UnityEngine;

namespace Game.Player.UI
{
    internal abstract class PlayerScreen : EcsComponentMono
    {
        [SerializeField] protected Transform _view;
        [SerializeField] protected EntityReference _reference;

        public void SetView(bool state) => _view.gameObject.SetActive(state);

        public bool IsActive => _view.gameObject.activeInHierarchy;
    }
}