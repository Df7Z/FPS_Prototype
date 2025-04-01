using System.Collections.Generic;
using UnityEngine;

namespace ECS_MONO
{
    internal sealed class SystemController<E> where E : class, IEntity
    {
        private int i, j, k, l, p;
        
        private List<IEcsSystem<E>> _systems;
        private List<IEcsSystem<E>> _updateSystems;
        private List<IEcsSystem<E>> _fixedSystems;
        private List<IEcsSystem<E>> _lateSystems;
        
       // private HashSet<IEcsSystem<E>> _activeSystems;
       // private HashSet<IEcsSystem<E>> _updateActiveSystems;
       // private HashSet<IEcsSystem<E>> _fixedActiveSystems;
       // private HashSet<IEcsSystem<E>> _lateActiveSystems;
        
        private GameObject _systemsTransform;


        public SystemController(GameObject systemsTransform)
        {
            _systemsTransform = systemsTransform;
            
            _systems = new List<IEcsSystem<E>>();
            _updateSystems = new List<IEcsSystem<E>>();
            _fixedSystems = new List<IEcsSystem<E>>();
            _lateSystems = new List<IEcsSystem<E>>();

           // _activeSystems = new HashSet<IEcsSystem<E>>();
          //  _updateActiveSystems = new HashSet<IEcsSystem<E>>();
          //  _fixedActiveSystems = new HashSet<IEcsSystem<E>>();
           // _lateActiveSystems = new HashSet<IEcsSystem<E>>();
        }

        public void EntityAddComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            for (p = 0; p < _systems.Count; p++)
            {
                _systems[p].TryRegisterEntityComponents(entity); //Пытаемся зарегистрировать эту сущьность в систему
            }
        }
        
        public void EntityDelComponent<T>(E entity, T component) where T : class, IEcsComponent
        {
            for (l = 0; l < _systems.Count; l++)
            {
                _systems[l].TryUnregisterEntityComponents(entity); //Пытаемся удалить сущьность из системы
            }
        }

        public void EntityDelFromWorld(E entity) 
        {
            for (l = 0; l < _systems.Count; l++)
            {
                _systems[l].SilentUnregisterEntity(entity); //Пытаемся удалить сущьность из системы
            }
        }

        public void EntityAddToWorld(E entity)
        {
            for (p = 0; p < _systems.Count; p++)
            {
                _systems[p].TryRegisterEntityComponents(entity); //Пытаемся зарегистрировать эту сущьность в систему
            }
        }

        public void Update()
        {
           // foreach (var system in _updateActiveSystems) system.UpdateSys();
            
            for (i = 0; i < _updateSystems.Count; i++)
            {
                _updateSystems[i].UpdateSys();
            }
        }
        
        public void FixedUpdate()
        {
           // foreach (var system in _fixedActiveSystems) system.FixedUpdateSys();
           
            for (j = 0; j < _fixedSystems.Count; j++)
            {
                _fixedSystems[j].FixedUpdateSys();
            }
        }
        
        public void LateUpdate()
        {
           // foreach (var system in _lateActiveSystems) system.LateUpdateSys();
           
            for (k = 0; k < _lateSystems.Count; k++)
            {
                _lateSystems[k].LateUpdateSys();
            }
        }

        public void Destruct(EcsWorldAbstract<E> world)
        {
            foreach (var system in _systems)
            {
                system.DestructFromWorld(world);
            }
        }
        
        private void CreateSystem<T>(List<IEcsSystem<E>> list, GameObject parent) where T : EcsSystemAbstract<E>
        {
            if (parent.TryGetComponent(out T system))
            {
                list.Add(system);

                return;
            }

            list.Add(parent.AddComponent<T>());
        }
        
        public void CreateUpdateSystem<T>() where T : EcsSystemAbstract<E> => CreateSystem<T>(_updateSystems, _systemsTransform);
        public void CreateFixedUpdateSystem<T>() where T : EcsSystemAbstract<E> => CreateSystem<T>(_fixedSystems, _systemsTransform);
        public void CreateLateUpdateSystem<T>() where T : EcsSystemAbstract<E> => CreateSystem<T>(_lateSystems, _systemsTransform);
        
        public void InitSystemsFromWorld(EcsWorldAbstract<E> world, IEcsCore core)
        {
            _systems.AddRange(_updateSystems);
            _systems.AddRange(_fixedSystems);
            _systems.AddRange(_lateSystems);
            
            foreach (var system in _systems)
            {
                system.InitFromWorld(world, core);
            }
        }

        public void OnCoreInitialize()
        {
            foreach (var system in _systems)
            {
                system.InitFromСore();
            }
        }
    }
}