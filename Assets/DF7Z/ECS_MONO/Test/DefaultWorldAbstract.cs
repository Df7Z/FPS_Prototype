using System.Collections.Generic;

using UnityEngine;

namespace ECS_MONO
{
    public class DefaultWorld : EcsWorldMono
    {
        public override WorldId ID => WorldId.Default;
        
        protected override void InitSystems()
        {
            
        }
    }
}