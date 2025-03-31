using System;
using UnityEngine;

namespace ECS_MONO
{
    public interface IEcsSystem<E> where E : class, IEntity
    {
        internal void UpdateSys();
        internal void FixedUpdateSys();
        internal void LateUpdateSys();
        internal void DestructFromWorld(EcsWorldAbstract<E> ecsWorldAbstract);
        internal void InitFromWorld(EcsWorldAbstract<E> ecsWorldAbstract, IEcsCore core);
        void TryRegisterEntityComponents(E entity);
        void TryUnregisterEntityComponents(E entity);
        void SilentUnregisterEntity(E entity);
    }

    public abstract class EcsSystemAbstract<E> : MonoBehaviour, IEcsSystem<E> where E : class, IEntity
    {
        protected EcsWorldAbstract<E> _world;
        
        protected const int DefaultInitComponentsCapacity = 8;
        protected const int DefaultComponentsAddCapacity = 2;
        protected const int DefaultComponentsDelCapacity = 2;
        
        protected Type[] _requiredTypes;
        
        void IEcsSystem<E>.DestructFromWorld(EcsWorldAbstract<E> ecsWorldAbstract)
        {
            Destruct();
        }
        
        void IEcsSystem<E>.InitFromWorld(EcsWorldAbstract<E> worldAbstract, IEcsCore core)
        {
            _world = worldAbstract;
            
            Init(core);
        }

        void IEcsSystem<E>.TryRegisterEntityComponents(E entity)
        {
            TryRegisterEntityComponents(entity);
        }

        void IEcsSystem<E>.TryUnregisterEntityComponents(E entity)
        {
            TryUnregisterEntityComponents(entity);
        }

       
        protected abstract void Init(IEcsCore core);
        
        public Type[] RequiredTypes => _requiredTypes;
        
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
        
        public abstract bool RequiredComponentType<T>();
        
        public abstract void SilentUnregisterEntity(E entity);

        public abstract bool TryRegisterEntityComponents(E entity);
        public abstract bool TryUnregisterEntityComponents(E entity);
        public abstract bool SilentUnregisterEntityComponents(E  entity);
        public abstract bool CanRegisterEntity(E  entity);
        public abstract bool CanUnregisterEntity(E  entity);
    }
}