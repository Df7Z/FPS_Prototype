using System;

namespace ECS_MONO
{
    public class EcsSystemAbstract<E, C1, C2> : EcsSystemBase<EcsNetBaseSystemComponents<E, C1, C2>, E>
        where E : class, IEntity
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
    {
        public override bool RequiredComponentType<T>() => 
            typeof(T) == typeof(C1) || typeof(T) == typeof(C2);

        public override bool CanRegisterEntity(E entityMono) =>
            entityMono.Has<C1>() && entityMono.Has<C2>();
        
        public override bool CanUnregisterEntity(E entityMono) =>
            !entityMono.Has<C1>() || !entityMono.Has<C2>();

        protected override void InitRequiredTypesForSystem(out Type[] types) => 
            types = new[] { typeof(C1), typeof(C2) };
        
        protected internal override void InitNewComponentField(in E entityMono, in EcsNetBaseSystemComponents<E, C1, C2> components)
        {
            components.Component1 = entityMono.Get<C1>();
            components.Component2 = entityMono.Get<C2>();
        }

        protected internal override void UpdateSystem(EcsNetBaseSystemComponents<E, C1, C2> components) => 
            Run(components.Entity, components.Component1, components.Component2);
        protected internal override void LateUpdateSystem(EcsNetBaseSystemComponents<E, C1, C2> components) => 
            LateRun(components.Entity, components.Component1, components.Component2);
        protected internal override void FixedUpdateSystem(EcsNetBaseSystemComponents<E, C1, C2> components) => 
            FixedRun(components.Entity, components.Component1, components.Component2);
        
        protected virtual void Run(E e, C1 c1, C2 c2) {}
        protected virtual void FixedRun(E e, C1 c1, C2 c2) {}
        protected virtual void LateRun(E e, C1 c1, C2 c2) {}
    }
}