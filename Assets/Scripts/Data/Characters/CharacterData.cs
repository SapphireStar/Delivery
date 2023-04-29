using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Datas
{
    public class CharacterData:BaseData,ICharacterData
    {
        [Header("Character Base Status")]
        [SerializeField]
        private float m_health = 100;
        public float Health { get => m_health; set => m_health = value; }

        [SerializeField]
        private float m_maxHealth = 100;
        public float MaxHealth { get => m_maxHealth; set => m_maxHealth = value; }

        [SerializeField]
        private float m_speed = 20;
        public float Speed { get => m_speed; set => m_speed = value; }

        [SerializeField]
        private float m_attack = 10;
        public float Attack { get => m_attack; set => m_attack = value; }
    }
}

