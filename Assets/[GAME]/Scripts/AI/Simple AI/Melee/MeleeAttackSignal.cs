﻿using ECS_MONO;
using Game.Damage;

namespace Game.AI
{
    internal sealed class MeleeAttackSignal : EcsComponent
    {
        public override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}