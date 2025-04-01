using ECS_MONO;

namespace Game.Damage
{
    public abstract class EnduranceTransaction : EcsComponent
    {
        public IEntity Source;
        public IEntity Target;
    }
    
    public sealed class DamageTransaction : EnduranceTransaction
    {
        public DamageData Data;
    }
    
    public sealed class HealTransaction : EnduranceTransaction
    {
        public HealData Data;
    }
}