using Isekai.Datas;
using Isekai.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
    public interface IComponentSystem
    {
        BaseData Data { get; }
        public T GetSubComponent<T>(EComponent component);
    }

}
