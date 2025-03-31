using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "SwaySettings", menuName = "Game/Player/Sway settings")]
    public class SwaySettings : ScriptableObject
    {
        public float MaxAngle = 45f;
        public float Smooth = 1f;
        public float MultiplierX = 1f;
        public float MultiplierY = 1f;
        public float OffsetSmooth = 1f;
        public float OffsetMultiplierX = 1f;
        public float OffsetMultiplierY = 1f;
    }
}