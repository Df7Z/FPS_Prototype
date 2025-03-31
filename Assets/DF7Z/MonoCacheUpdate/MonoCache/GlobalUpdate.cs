// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2023 Night Train Code
// -------------------------------------------------------------------------------------------

using System.Collections.Generic;
using MonoSystemCache.Interfaces;
using UnityEngine;

namespace MonoSystemCache
{
 
    [DisallowMultipleComponent]
    public sealed class GlobalUpdate : MonoBehaviour //: Singleton<GlobalUpdate>
    {
        public const string OnEnableMethodName = "OnEnable";
        public const string OnDisableMethodName = "OnDisable";
        
        public const string UpdateMethodName = nameof(Update);
        public const string FixedUpdateMethodName = nameof(FixedUpdate);
        public const string LateUpdateMethodName = nameof(LateUpdate);

        private readonly List<IRunSystem> _runSystems = new List<IRunSystem>(1024);
        private readonly List<IFixedRunSystem> _fixedRunSystems = new List<IFixedRunSystem>(512);
        private readonly List<ILateRunSystem> _lateRunSystems = new List<ILateRunSystem>(256);

        private readonly MonoCacheExceptionsChecker _monoCacheExceptionsChecker = 
            new MonoCacheExceptionsChecker();
        
        #region SINGLETONE
        
        public static GlobalUpdate Instance;

        private void Awake() {
         
            if (Instance == null) {

                DontDestroyOnLoad(this);
                Instance = this;
                transform.parent = null;
                
            }
            else if (Instance != this) {
                Destroy(gameObject);
            }
           
        }

        #endregion
        
        
         //   _monoCacheExceptionsChecker.CheckForExceptions();
        
        public void AddRunSystem(IRunSystem runSystem)
        {
            _runSystems.Add(runSystem);
        }

        public void AddFixedRunSystem(IFixedRunSystem fixedRunSystem)
        {
            _fixedRunSystems.Add(fixedRunSystem);
        }

        public void AddLateRunSystem(ILateRunSystem lateRunSystem)
        {
            _lateRunSystems.Add(lateRunSystem);
        }

        public void RemoveRunSystem(IRunSystem runSystem)
        {
            _runSystems.Remove(runSystem);
        }

        public void RemoveFixedRunSystem(IFixedRunSystem fixedRunSystem)
        {
            _fixedRunSystems.Remove(fixedRunSystem);
        }
        
        public void RemoveLateRunSystem(ILateRunSystem lateRunSystem)
        {
            _lateRunSystems.Remove(lateRunSystem);
        }

        private static int __i;
        private void Update()
        {
            for (__i = 0; __i < _runSystems.Count; __i++)
            {
                _runSystems[__i].OnRun();
            }
        }

        private void FixedUpdate()
        {
            for (__i = 0; __i < _fixedRunSystems.Count; __i++)
            {
                _fixedRunSystems[__i].OnFixedRun();
            }
        }

        private void LateUpdate()
        {
            for (__i = 0; __i < _lateRunSystems.Count; __i++)
            {
                _lateRunSystems[__i].OnLateRun();
            }
        }
    }
}