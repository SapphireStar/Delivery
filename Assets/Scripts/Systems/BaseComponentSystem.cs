using Isekai.Components;
using Isekai.Datas;
using Isekai.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
    public abstract class BaseComponentSystem : MonoBehaviour, IComponentSystem
    {
        [SerializeField]
        public BaseData m_data;
        public BaseData Data { get => m_data; }

        [SerializeField, Header("LoadedComponents")]
        protected EComponent[] m_components;

        protected Dictionary<EComponent, IComponent> compoentDict = new Dictionary<EComponent, IComponent>();

        protected virtual void Awake()
        {
            foreach (var item in m_components)
            {
                IComponent component = ComponentProvider.GetComponent(item);
                compoentDict[item] = component;
            }
            InitializeComponents();
        }
        protected virtual void InitializeComponents()
        {
            foreach (var item in compoentDict)
            {
                item.Value.Initialize(this);
            }
        }

        public T GetSubComponent<T>(EComponent component)
        {
            if (compoentDict.ContainsKey(component))
            {
                return (T)compoentDict[component];
            }
            else
            {
                return default(T);
            }
        }
    }
}

