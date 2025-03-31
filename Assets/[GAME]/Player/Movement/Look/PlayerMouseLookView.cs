using ECS_MONO;
using UnityEngine;

namespace Game.Player.Look
{
    internal sealed class PlayerMouseLookView : EcsComponentMono
    {
        [SerializeField] private Transform _player; //Поворачиваем
        [SerializeField] private Transform _view; //Точка обзора
        public Transform Player => _player;
        public Transform View => _view;
    }
}