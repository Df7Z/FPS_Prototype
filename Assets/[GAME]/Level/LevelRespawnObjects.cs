using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ECS_MONO;
using Game.Level.Shared;
using PoolSystem;
using UnityEngine;

namespace Game.Level
{
    internal class LevelRespawnObjects
    {
        private class Respawn
        {
            public Vector3 Pos;
            public Quaternion Rot;
            public EntityMono Prefab;
        }

        private List<Respawn> _respawns = new List<Respawn>();

        private GameObject _gameObjectRespawn;
        public GameObject RespawnObject => _gameObjectRespawn;
        
        public void Init(LevelRespawnObject[] respawnObjects)
        {
            _gameObjectRespawn = new GameObject("[RESPAWN]");
            
            foreach (var respawnObject in respawnObjects)
            {
                _respawns.Add(new Respawn()
                {
                    Pos = respawnObject.transform.position,
                    Rot = respawnObject.transform.rotation,
                    Prefab = respawnObject.Data.Prefab
                });
                
                respawnObject.transform.parent = _gameObjectRespawn.transform;
            }
        }

        public void Restart(LevelRespawnObject[] respawnObjects) //Остались
        {
            foreach (var respawnObject in respawnObjects)
            {
                SystemPool.Despawn(respawnObject.gameObject);
            }
            
            foreach (var respawnObject in _respawns)
            {
                var obj = SystemPool.Spawn(respawnObject.Prefab, respawnObject.Pos, respawnObject.Rot);

                obj.transform.parent = _gameObjectRespawn.transform;
            }
        }
    }
}