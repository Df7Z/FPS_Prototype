using System;


namespace ECS_MONO
{
    public class EcsSystem<C1> : EcsSystemAbstract<Entity, C1>
        where C1 : class, IEcsComponent
    {
    }
    
    public class EcsSystem<C1, C2> : EcsSystemAbstract<Entity, C1, C2>
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
    {
    }
    
    public class EcsSystem<C1, C2, C3> : EcsSystemAbstract<Entity, C1, C2, C3>
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
        where C3 : class, IEcsComponent
    {
    }
}