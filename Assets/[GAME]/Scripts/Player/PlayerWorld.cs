using System;
using System.Collections.Generic;
using ECS_MONO;
using Game.Player.Dead;
using Game.Player.Look;
using Game.Player.Move;
using Game.Player.Teleport;
using Game.Player.Test;
using Game.Player.UI;
using UnityEngine;

namespace Game.Player
{
    public class PlayerWorld : EcsWorldMono
    {
        public override WorldId ID => WorldId.Player;

        protected override void InitSystems()
        {
            CreateUpdateSystem<PlayerSpawnSystem>(); //Instantiate
         
            CreateUpdateSystem<PlayerDeadScreenSystem>();
            CreateUpdateSystem<PlayerFinishScreenSystem>();
            
            CreateUpdateSystem<PlayerChangeCursorSystem>();
            CreateUpdateSystem<PlayerChangeInputStateSystemMono>();
            CreateUpdateSystem<PlayerDefaultInputSystemMono>();
            CreateUpdateSystem<PlayerBlockInputSystemMono>();
            CreateUpdateSystem<PlayerInventoryInputSystem>();
         
            CreateUpdateSystem<PlayerFinishLevelSystem>();
            CreateUpdateSystem<PlayerDeadSystem>();
            CreateUpdateSystem<PlayerRespawnSystem>();
            CreateUpdateSystem<TeleportHandleSystem>();

            CreateUpdateSystem<PlayerMouseLookChangeSystem>();
            CreateUpdateSystem<PlayerMouseLookSystemMono>();
            
            CreateUpdateSystem<PlayerMoveSystemMono>();
            CreateUpdateSystem<PlayerGroundMoveSystemMono>();
            CreateUpdateSystem<PlayerAirMoveSystemMono>();
            CreateUpdateSystem<PlayerMoveCameraSystemMono>();
            
            CreateUpdateSystem<TestInput>();
           
        }

        protected override void InitPools(in Dictionary<Type, IEcsWorldComponentPoolBase> p)
        {
            p.Add(typeof(PlayerInput), new EcsWorldComponentPool<PlayerInput>());
            p.Add(typeof(PlayerTag), new EcsWorldComponentPool<PlayerTag>());
        }
    }
}