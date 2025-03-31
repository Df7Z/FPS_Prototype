using ECS_MONO;
using Game.Damage;
using UnityEngine;

namespace Game.Player.Test
{
    public class TestInput : EcsSystemMono<PlayerInput>
    {
        protected override void Run(EntityMono e, PlayerInput c1)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                var damage = new DamageData();
                
                damage.CreateTransaction(e.Get<Damaged>(), e);
            }
            
            if (Input.GetKey(KeyCode.Z))
            {
                var damage = new DamageData();
                
                damage.CreateTransaction(e.Get<Damaged>(), e);
            }
        }
    }
}