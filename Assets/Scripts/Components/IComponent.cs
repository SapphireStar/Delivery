using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Components
{
    public interface IComponent
    {
        void Initialize(BaseComponentSystem system);
   }

}
