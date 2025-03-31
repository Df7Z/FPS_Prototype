using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ECS_MONO
{
    public abstract class EcsCore : MonoBehaviour, IEcsCore
    {
        public static Action OnInitialized;
        
        public abstract IEcsWorld<T> GetWorld<T>(WorldId id) where T : class, IEntity;
        
        protected abstract void Init();
        protected abstract void Destruct();
        
        #region SINGLETONE

        public static EcsCore I;

        private void Awake() {
            SINGLETONE();
        }
    
        private void SINGLETONE() {
            if (I == null) {
                I = this;
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                
                EntityPool.InitGenerator(new Func<Entity>(() => new Entity()));
                
                Init();
                
                OnInitialized?.Invoke();
            }
            else if (I != this) {
                Destroy(gameObject);
            }
        }
        
        #endregion
        
        private void OnDestroy()
        {
            Destruct();
        }
    }
}