using Cysharp.Threading.Tasks;
using Isekai.Datas;
using Isekai.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
    [Serializable]
    public abstract class BaseSystem : MonoBehaviour, ISystem
    {
        public IData data;
        protected BaseComponentSystem m_componentSystem;
        public BaseSystem()
        {

        }

        protected virtual void Start()
        {
            m_componentSystem = gameObject.GetComponent<BaseComponentSystem>();
            Initialize();
            BindComponentProperty();
            BindComponentPropertyAsync().Forget();
        }

        protected virtual void OnDestroy()
        {
            OnRemove();
            UnbindComponentProperty();
        }
        protected T GetSubComponent<T>(EComponent component)
        {
            return m_componentSystem.GetSubComponent<T>(component);
        }

        public abstract void Initialize();

        public abstract void OnRemove();
        public abstract UniTaskVoid BindComponentPropertyAsync();
        public abstract void BindComponentProperty();
        public abstract void UnbindComponentProperty();
    }
}

