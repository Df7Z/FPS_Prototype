using System;
using UnityEngine;

namespace ECS_MONO
{
    public abstract class EcsSystemAbstract<E> : MonoBehaviour, IEcsSystem<E> where E : class, IEntity
    {
        protected EcsWorldAbstract<E> _world;
        
        internal const int DefaultInitComponentsCapacity = 8;
        internal const int DefaultComponentsAddCapacity = 2;
        internal const int DefaultComponentsDelCapacity = 2;
        
        internal Type[] _requiredTypes;
        
        void IEcsSystem<E>.DestructFromWorld(EcsWorldAbstract<E> ecsWorldAbstract)
        {
            Destruct();
        }
        
        void IEcsSystem<E>.InitFromWorld(EcsWorldAbstract<E> worldAbstract, IEcsCore core)
        {
            _world = worldAbstract;
            
            InitSystem(core);
        }

        protected virtual void Init() {}
        
        void IEcsSystem<E>.InitFromСore()
        {
            Init(); //Все миры, системы, ядро готово
        }
        void IEcsSystem<E>.TryRegisterEntityComponents(E entity)
        {
            TryRegisterEntityComponents(entity);
        }
        
        void IEcsSystem<E>.TryUnregisterEntityComponents(E entity)
        {
            TryUnregisterEntityComponents(entity);
        }

       
        internal abstract void InitSystem(IEcsCore core);
        
        internal Type[] RequiredTypes => _requiredTypes;
        
        internal abstract void UpdateSys();
        void IEcsSystem<E>.FixedUpdateSys()
        {
            FixedUpdateSys();
        }

        void IEcsSystem<E>.LateUpdateSys()
        {
            LateUpdateSys();
        }

        void IEcsSystem<E>.UpdateSys()
        {
            UpdateSys();
        }
        
        internal abstract void FixedUpdateSys();
        internal abstract void LateUpdateSys();
        internal void Destruct()
        {
            OnDestruct();
        }
        protected virtual void OnDestruct() {}
        
        void IEcsSystem<E>.SilentUnregisterEntity(E entity)
        {
            TryUnregisterEntityComponents(entity);
        }
        
        internal abstract bool RequiredComponentType<T>();
        internal abstract void SilentUnregisterEntity(E entity);
        internal abstract bool TryRegisterEntityComponents(E entity);
        internal abstract bool TryUnregisterEntityComponents(E entity);
        internal abstract bool SilentUnregisterEntityComponents(E  entity);
        internal abstract bool CanRegisterEntity(E  entity);
        internal abstract bool CanUnregisterEntity(E  entity);
    }
}