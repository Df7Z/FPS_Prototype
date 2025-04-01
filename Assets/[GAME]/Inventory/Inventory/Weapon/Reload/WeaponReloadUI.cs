using System;
using ECS_MONO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Inventory
{
    internal sealed class WeaponReloadUI : EcsComponentMono
    {
        [SerializeField] private TextMeshProUGUI _outCurrent;
        [FormerlySerializedAs("_outMax")] [SerializeField] private TextMeshProUGUI _outInventory;

        public void SetCurrent(string text) => _outCurrent.text = text;

        private void SetInventory(string text) => _outInventory.text = text;

        protected override void OnRegisterEntity(IEntity entity)
        {
            SetInventory(string.Empty);
        }
    }
}