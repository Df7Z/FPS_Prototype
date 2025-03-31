using UnityEngine;

namespace ECS_MONO
{
    public class EntityAnimator : EcsComponentMono
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;

        public void Play(EntityAnimation animation)
        {
            animation.Play(_animator);
        }
    }
}