using ECS_MONO;
using TMPro;
using UnityEngine;

namespace Game.Damage
{
    internal sealed class DamagedUIView : EcsComponentMono
    {
        [SerializeField] private TextMeshProUGUI _out;

        public void SetCountText(string text) => _out.text = text;
    }
}