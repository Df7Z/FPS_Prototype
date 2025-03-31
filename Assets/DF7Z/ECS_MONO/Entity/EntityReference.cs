using UnityEngine;

namespace ECS_MONO
{
    public abstract class EntityReference : MonoBehaviour
    {
        public abstract IEntity Entity { get; }
    }
}