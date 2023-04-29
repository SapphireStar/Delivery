using FSM;
using Isekai.Components;
using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.StateMachines
{
    public class PlayerDodgeState : State
    {
        private BaseComponentSystem m_componentSystem;

        private CombatComponent m_combatComponent;
        public PlayerDodgeState(BaseComponentSystem componentSystem)
        {
            m_componentSystem = componentSystem;
            m_combatComponent = componentSystem.GetSubComponent<CombatComponent>(Factories.EComponent.CombatComponent);
        }
        public override void OnEnter()
        {
            base.OnEnter();
            if (m_combatComponent != null)
            {
                m_combatComponent.IsAttacking = false;
                m_combatComponent.CanAttack = false;
            }
            Debug.Log(m_combatComponent.CanAttack);
        }
        public override void OnExit()
        {
            base.OnExit();
            if (m_combatComponent != null)
            {
                m_combatComponent.CanAttack = true;
            }
            Debug.Log(m_combatComponent.CanAttack);
        }
    }

}
