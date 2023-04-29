using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Datas
{

    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Characters/PlayerData", order = 1), Serializable]
    public class PlayerData : CharacterData, IPlayerData
    {
        //Player Base Status
        [Header("Player Base Status")]
        [SerializeField]
        private float m_dodgeSpeed = 250;
        public float DodgeSpeed { get => m_dodgeSpeed; set => m_dodgeSpeed = value; }

        [SerializeField]
        private float m_minimumDodgeSpeed = 50;
        public float MinimumDodgeSpeed { get => m_minimumDodgeSpeed; set => m_minimumDodgeSpeed = value; }

        [SerializeField]
        private float m_attackBuffer = 0.5f;
        public float AttackBuffer { get => m_attackBuffer; set => m_attackBuffer = value; }

        [SerializeField]
        private float m_jumpHeight = 5;
        public float JumpHeight { get => m_jumpHeight; set => m_jumpHeight = value; }
    }
}