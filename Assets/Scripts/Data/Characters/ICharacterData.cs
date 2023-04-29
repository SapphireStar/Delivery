using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Datas
{
    public interface ICharacterData : IData
    {
        float Health { get; set; }
        float MaxHealth { get; set; }
        float Speed { get; set; }
        float Attack { get; set; }
    }

}
