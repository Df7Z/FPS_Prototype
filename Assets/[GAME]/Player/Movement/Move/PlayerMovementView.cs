using ECS_MONO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Player.Move
{
    internal class PlayerMovementView : EcsComponentMono
    {
        [SerializeField] private PlayerMovementData data;

        public PlayerMovementData Data => data;

        [SerializeField] protected Transform _mesh;
        [SerializeField] protected Transform _playerModel;
        [SerializeField] protected Transform _viewTransform;
        [SerializeField] protected Transform _viewMouseLook;
        [SerializeField] protected CharacterController _characterController;
        public Transform Mesh => _mesh;
        public Transform ViewMouseLook => _viewMouseLook;
        public Transform PlayerModel => _playerModel;
        public Transform ViewTransform => _viewTransform;
        public CharacterController CharacterController => _characterController;
    }
}