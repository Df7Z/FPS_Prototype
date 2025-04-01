using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class WeaponAiming : EcsComponentMono
    {
        [SerializeField] private float _fovChange;
        [SerializeField] private Transform _aimPosition;
        
        public float FOVChange => _fovChange;
        public Transform AimPosition => _aimPosition;
    }
}