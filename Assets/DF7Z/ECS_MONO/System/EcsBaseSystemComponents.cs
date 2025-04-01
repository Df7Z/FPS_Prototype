
namespace ECS_MONO
{
    public class EcsBaseSystemComponents<E> where E : class, IEntity
    {
        public E Entity;
    }
    
    public class EcsBaseSystemComponents<E, C1> : EcsBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
    {
        public C1 Component1;
    }
    
    public class EcsBaseSystemComponents<E, C1, C2> : EcsBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
        where C2 : IEcsComponent
    {
        public C1 Component1;
        public C2 Component2;
    }
    
    public class EcsBaseSystemComponents<E, C1, C2, C3> : EcsBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
        where C2 : IEcsComponent
        where C3 : IEcsComponent
    {
        public C1 Component1;
        public C2 Component2;
        public C3 Component3;
    }
    
    public class EcsBaseSystemComponents<E, C1, C2, C3, C4> : EcsBaseSystemComponents<E> where E : class, IEntity 
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
    
    public class EcsBaseSystemComponents<E, C1, C2, C3, C4, C5> : EcsBaseSystemComponents<E> where E : class, IEntity 
        where C1 : IEcsComponent
        where C2 : IEcsComponent
        where C3 : IEcsComponent
        where C4 : IEcsComponent
        where C5 : IEcsComponent
    {
        public C1 Component1;
        public C2 Component2;
        public C3 Component3;
        public C4 Component4;
        public C5 Component5;
    }
}