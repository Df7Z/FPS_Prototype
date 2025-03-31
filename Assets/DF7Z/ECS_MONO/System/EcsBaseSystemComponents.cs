
namespace ECS_MONO
{
    public class EcsNetBaseSystemComponents<E> where E : class, IEntity
    {
        public E Entity;
    }
    
    public class EcsNetBaseSystemComponents<E, C1> : EcsNetBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
    {
        public C1 Component1;
    }
    
    public class EcsNetBaseSystemComponents<E, C1, C2> : EcsNetBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
        where C2 : IEcsComponent
    {
        public C1 Component1;
        public C2 Component2;
    }
    
    public class EcsNetBaseSystemComponents<E, C1, C2, C3> : EcsNetBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
        where C2 : IEcsComponent
        where C3 : IEcsComponent
    {
        public C1 Component1;
        public C2 Component2;
        public C3 Component3;
    }
    
    public class EcsNetBaseSystemComponents<E, C1, C2, C3, C4> : EcsNetBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
        where C2 : IEcsComponent
        where C3 : IEcsComponent
        where C4 : IEcsComponent
    {
        public C1 Component1;
        public C2 Component2;
        public C3 Component3;
        public C4 Component4;
    }
}