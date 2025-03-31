using System;


namespace ECS_MONO
{
    public class EcsSystemMono<C1> : EcsSystemAbstract<EntityMono, C1>
        where C1 : class, IEcsComponent
    {
    }
    
    public class EcsSystemMono<C1, C2> : EcsSystemAbstract<EntityMono, C1, C2>
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
    {
    }
    
    public class EcsSystemMono<C1, C2, C3> : EcsSystemAbstract<EntityMono, C1, C2, C3>
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
        where C3 : class, IEcsComponent
    {
    }
    
    public class EcsSystemMono<C1, C2, C3, C4> : EcsSystemAbstract<EntityMono, C1, C2, C3, C4>
        where C1 : class, IEcsComponent
        where C2 : class, IEcsComponent
        where C3 : class, IEcsComponent
        where C4 : class, IEcsComponent
    {
    }
}