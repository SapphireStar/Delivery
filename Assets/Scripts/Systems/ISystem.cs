using Isekai.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
    public interface ISystem
    {
        void Initialize();

        void BindComponentProperty();
        void OnRemove();


    }
}

