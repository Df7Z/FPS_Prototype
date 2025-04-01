using ECS_MONO;
using TMPro;
using UnityEngine;

namespace Game.Damage
{
    internal sealed class Damaged3DUIView : EcsComponentMono
    {
        [SerializeField] private TextMeshPro _out;

        public void SetCountText(string text) => _out.text = text;
    }
}