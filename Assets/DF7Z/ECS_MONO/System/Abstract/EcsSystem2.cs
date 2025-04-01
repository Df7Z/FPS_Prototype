using System;

namespace ECS_MONO
{
    public class EcsSystemAbstract<E, C1, C2> : EcsSystemBase<EcsBaseSystemComponents<E, C1, C2>, E>
        where E : class, IEntity
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
    {
        internal override bool RequiredComponentType<T>() => 
            typeof(T) == typeof(C1) || typeof(T) == typeof(C2);

        internal override bool CanRegisterEntity(E entityMono) =>
            entityMono.Has<C1>() && entityMono.Has<C2>();
        
        internal override bool CanUnregisterEntity(E entityMono) =>
            !entityMono.Has<C1>() || !entityMono.Has<C2>();

        internal override void InitRequiredTypesForSystem(out Type[] types) => 
            types = new[] { typeof(C1), typeof(C2) };
        
        internal override void InitNewComponentField(in E entityMono, in EcsBaseSystemComponents<E, C1, C2> components)
        {
            components.Component1 = entityMono.Get<C1>();
            components.Component2 = entityMono.Get<C2>();
        }

        internal override void UpdateSystem(EcsBaseSystemComponents<E, C1, C2> components) => 
            Run(components.Entity, components.Component1, components.Component2);
        internal override void LateUpdateSystem(EcsBaseSystemComponents<E, C1, C2> components) => 
            LateRun(components.Entity, components.Component1, components.Component2);
        internal override void FixedUpdateSystem(EcsBaseSystemComponents<E, C1, C2> components) => 
            FixedRun(components.Entity, components.Component1, components.Component2);
        
        protected virtual void Run(E e, C1 c1, C2 c2) {}
        protected virtual void FixedRun(E e, C1 c1, C2 c2) {}
        protected virtual void LateRun(E e, C1 c1, C2 c2) {}
    }
}