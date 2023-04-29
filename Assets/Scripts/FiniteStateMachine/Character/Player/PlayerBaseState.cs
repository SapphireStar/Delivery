using FSM;
using Isekai.Components;
using Isekai.Factories;
using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.StateMachines
{
    public class PlayerBaseState : State
    {
        private PlayerComponentSystem m_componentSystem;

        private CombatComponent m_combatComponent;
        public PlayerBaseState(PlayerComponentSystem componentSystem)
        {
            m_componentSystem = componentSystem;

            m_combatComponent = m_componentSystem.GetSubComponent<CombatComponent>(EComponent.CombatComponent);
        }
        public override void OnEnter()
        {
            base.OnEnter();
            if (m_combatComponent != null)
            {
                m_combatComponent.CanAttack = true;

            }

        }
    }

}
