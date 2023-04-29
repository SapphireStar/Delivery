using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Datas
{
    public interface IPlayerData:IData
    {
        float Health { get; set; }
        float MaxHealth { get; set; }
        float Speed { get; set; }
        float Attack { get; set; }
        float DodgeSpeed { get; set; }
        float JumpHeight { get; set; }
        public float MinimumDodgeSpeed { get; set; }
        public float AttackBuffer { get; set; }
    }
}

