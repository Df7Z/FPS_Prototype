using System;
using System.Collections.Generic;

using Game.Damage;
using Game.Inventory;
using Game.Mob;
using Game.Mob.AI;
using Game.Mobs;
using Game.Player;
using Game.Projectile;


namespace ECS_MONO
{
    public class EcsGameCore : EcsCore
    {
        private Dictionary<WorldId, EcsWorldMono> _monoWorlds;
        private Dictionary<WorldId, EcsWorld> _worlds;
        
        private void InitMonoWorlds(List<EcsWorldMono> worlds)
        {
            worlds.Add(gameObject.AddComponent<DefaultWorld>());
            worlds.Add(gameObject.AddComponent<PlayerWorld>());
            worlds.Add(gameObject.AddComponent<InventoryWorld>());
            worlds.Add(gameObject.AddComponent<AIWorld>());
            worlds.Add(gameObject.AddComponent<MobWorld>());
            worlds.Add(gameObject.AddComponent<ProjectileWorld>());
        }

        private void InitWorlds(List<EcsWorld> worlds)
        {
            worlds.Add(gameObject.AddComponent<DamageWorld>());
           
        }
        
        protected override void Init()
        {
            List<EcsWorldMono> monoWorlds = new List<EcsWorldMono>();
            InitMonoWorlds(monoWorlds);
            _monoWorlds = new Dictionary<WorldId, EcsWorldMono>();
            foreach (var world in monoWorlds)
            {
                _monoWorlds.Add(world.ID, world);
                
                world.InitWorld(this);
            }
            
            List<EcsWorld> worlds = new List<EcsWorld>();
            InitWorlds(worlds);
            _worlds = new Dictionary<WorldId, EcsWorld>();
            foreach (var world in worlds)
            {
                _worlds.Add(world.ID, world);
                
                world.InitWorld(this);
            }
        }
        
        protected override void Destruct()
        {
            foreach (var world in _monoWorlds.Values)
            {
                world.Destruct();
            }
        }
        
        private void OnDestroy()
        {
            Destruct();
        }

        public override IEcsWorld<T> GetWorld<T>(WorldId id)
        {
            if (typeof(T) == typeof(EntityMono) && _monoWorlds.TryGetValue(id, out var world)) return (IEcsWorld<T>) world;
            if (typeof(T) == typeof(Entity) && _worlds.TryGetValue(id, out var world1)) return (IEcsWorld<T>) world1;
            
            throw new Exception("World not found!");
        }
    }
}