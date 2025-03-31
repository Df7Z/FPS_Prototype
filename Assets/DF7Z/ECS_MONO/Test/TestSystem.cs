using UnityEngine;

namespace ECS_MONO
{
    internal sealed class TestSystemMono : EcsSystemMono<TestComponentMono, TestComponent3>
    {
        protected override void Run(EntityMono e, TestComponentMono c1, TestComponent3 c2)
        {
            var result = e.Get<TestComponentMono>();
            
            var componentNew = e.AddMono<TestComponent2>();

            e.Del<TestComponent3>();
            
            e.Del<TestComponentMono>();
            
            Debug.Log("Test complete!");
        }
    }
}