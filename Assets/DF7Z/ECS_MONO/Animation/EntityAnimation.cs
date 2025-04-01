using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ECS_MONO
{
    [Serializable]
    public class EntityAnimation
    {
        public bool HasClips => _clipNames != null && _clipNames.Length > 0;
        
        public void SetClipNames(string[] n) => _clipNames = n;
        
        [SerializeField] private string[] _clipNames;
        
        private int[] AnimationHashes;
        public int Hash => AnimationHashes[Random.Range(0, AnimationHashes.Length)];
        public string[] GetClips => _clipNames;

        public void Play(Animator animator, ref float crossTime, ref int layer ,ref float timeOffset) {
            animator.CrossFade(Hash, crossTime, layer, timeOffset);
        }
        
        public void Play(Animator animator, ref float crossTime) {
            animator.CrossFade(Hash, crossTime);
        }

        public void Play(Animator animator) {
            animator.CrossFade(Hash, 0.1f, 0, 0f);
        }

        public void Play(Animator animator, int layer) {
            animator.CrossFade(Hash, 0.1f, layer, 0f);
        }
        
        public void MakeHash()
        {
            if (!HasClips) return;
            
            AnimationHashes = new int[_clipNames.Length];

            for (int i = 0; i < _clipNames.Length; i++)
            {
                AnimationHashes[i] = Animator.StringToHash(_clipNames[i]);
            }
        }
    }

}