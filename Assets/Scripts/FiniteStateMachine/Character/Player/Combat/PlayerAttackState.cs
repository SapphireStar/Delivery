using FSM;
using Isekai.Components;
using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.StateMachines 
{
    public class PlayerAttackState : State
    {
        private BaseWeaponSystem m_weapon;
        public PlayerAttackState(BaseWeaponSystem weapon)
        {
            m_weapon = weapon;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_weapon.EnterWeapon();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
            m_weapon.ExitWeapon();
        }

        public void SetWeapon(BaseWeaponSystem weapon)
        {
            if (m_weapon != null)
            {
                m_weapon.ExitWeapon();
            }
            m_weapon = weapon;
        }
    }

}
