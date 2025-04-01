using System;
using ECS_MONO;
using UnityEngine;

namespace Game.Player
{
    internal sealed class PlayerChangeInputStateSystemMono : EcsSystemMono<PlayerInput, ChangeInputStateSignal>
    {
        protected override void Run(EntityMono e, PlayerInput c1, ChangeInputStateSignal c2)
        {
            var target = c2.Target;
            
            e.SafeDel<InputDefaultState>();
            e.SafeDel<InputBlockState>();
            e.SafeDel<InputInventoryState>();

            if (Change<InputDefaultState>(e, target, PlayerInputState.Default)) return;
            if (Change<InputBlockState>(e, target, PlayerInputState.Block)) return;
            if (Change<InputInventoryState>(e, target, PlayerInputState.Inventory)) return;
            
            throw new Exception($"State {target} not found for ChangeInputStateSystem !");
        }

        private bool Change<T>(EntityMono e, PlayerInputState target, PlayerInputState typeState) where T : InputState, new()
        {
            if (target != typeState) return false;
            
            e.Add<T>();
            
            e.Del<ChangeInputStateSignal>();

            return true;
        }
    }
}