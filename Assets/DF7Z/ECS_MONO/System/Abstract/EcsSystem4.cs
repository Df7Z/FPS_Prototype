using System;

namespace ECS_MONO
{
    public class EcsSystemAbstract<E, C1, C2, C3, C4> : EcsSystemBase<EcsBaseSystemComponents<E, C1, C2, C3, C4>, E>
        where E : class, IEntity
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
        where C3 : class, IEcsComponent
        where C4 : class, IEcsComponent
    {
        internal override bool RequiredComponentType<T>() => 
            typeof(T) == typeof(C1) || typeof(T) == typeof(C2) || typeof(T) == typeof(C3) || typeof(T) == typeof(C4);

        internal override bool CanRegisterEntity(E entity) =>
            entity.Has<C1>() && entity.Has<C2>() && entity.Has<C3>() && entity.Has<C4>();
        
        internal override bool CanUnregisterEntity(E entity) =>
            !entity.Has<C1>() || !entity.Has<C2>() || !entity.Has<C3>() || !entity.Has<C4>();

        internal override void InitRequiredTypesForSystem(out Type[] types) => 
            types = new[] { typeof(C1), typeof(C2), typeof(C3), typeof(C4) };
        
        internal override void InitNewComponentField(in E entity, in EcsBaseSystemComponents<E, C1, C2, C3, C4> components)
        {
            components.Component1 = entity.Get<C1>();
            components.Component2 = entity.Get<C2>();
            components.Component3 = entity.Get<C3>();
            components.Component4 = entity.Get<C4>();
        }

        internal override void UpdateSystem(EcsBaseSystemComponents<E, C1, C2, C3, C4> components) => 
            Run(components.Entity, components.Component1, components.Component2, components.Component3, components.Component4);
        internal override void LateUpdateSystem(EcsBaseSystemComponents<E, C1, C2, C3, C4> components) => 
            LateRun(components.Entity, components.Component1, components.Component2, components.Component3, components.Component4);
        internal override void FixedUpdateSystem(EcsBaseSystemComponents<E, C1, C2, C3, C4> components) => 
            FixedRun(components.Entity, components.Component1, components.Component2, components.Component3, components.Component4);
        
        protected virtual void Run(E e, C1 c1, C2 c2, C3 c3, C4 c4) {}
        protected virtual void FixedRun(E e, C1 c1, C2 c2, C3 c3, C4 c4) {}
        protected virtual void LateRun(E e, C1 c1, C2 c2, C3 c3, C4 c4) {}
    }
}