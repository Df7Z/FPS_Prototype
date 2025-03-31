using UnityEngine;

namespace ECS_MONO
{
    public class EntityMonoReference : EntityReference
    {  
        [SerializeField] private EntityMono _entity;

        public override IEntity Entity => _entity;
    }
}