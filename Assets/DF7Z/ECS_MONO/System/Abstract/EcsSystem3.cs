using System;

namespace ECS_MONO
{
    public class EcsSystemAbstract<E, C1, C2, C3> : EcsSystemBase<EcsNetBaseSystemComponents<E, C1, C2, C3>, E>
        where E : class, IEntity
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
        where C3 : class, IEcsComponent
    {
        public override bool RequiredComponentType<T>() => 
            typeof(T) == typeof(C1) || typeof(T) == typeof(C2) || typeof(T) == typeof(C3);

        public override bool CanRegisterEntity(E entityMono) =>
            entityMono.Has<C1>() && entityMono.Has<C2>() && entityMono.Has<C3>();
        
        public override bool CanUnregisterEntity(E entityMono) =>
            !entityMono.Has<C1>() || !entityMono.Has<C2>() || !entityMono.Has<C3>();

        protected override void InitRequiredTypesForSystem(out Type[] types) => 
            types = new[] { typeof(C1), typeof(C2), typeof(C3) };
        
        protected internal override void InitNewComponentField(in E entityMono, in EcsNetBaseSystemComponents<E, C1, C2, C3> components)
        {
            components.Component1 = entityMono.Get<C1>();
            components.Component2 = entityMono.Get<C2>();
            components.Component3 = entityMono.Get<C3>();
        }

        protected internal override void UpdateSystem(EcsNetBaseSystemComponents<E, C1, C2, C3> components) => 
            Run(components.Entity, components.Component1, components.Component2, components.Component3);
        protected internal override void LateUpdateSystem(EcsNetBaseSystemComponents<E, C1, C2, C3> components) => 
            LateRun(components.Entity, components.Component1, components.Component2, components.Component3);
        protected internal override void FixedUpdateSystem(EcsNetBaseSystemComponents<E, C1, C2, C3> components) => 
            FixedRun(components.Entity, components.Component1, components.Component2, components.Component3);
        
        protected virtual void Run(E e, C1 c1, C2 c2, C3 c3) {}
        protected virtual void FixedRun(E e, C1 c1, C2 c2, C3 c3) {}
        protected virtual void LateRun(E e, C1 c1, C2 c2, C3 c3) {}
    }
}