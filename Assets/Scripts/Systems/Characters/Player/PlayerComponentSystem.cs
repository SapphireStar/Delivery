using Cysharp.Threading.Tasks;
using FSM;
using Isekai.Components;
using Isekai.Datas;
using Isekai.Factories;
using Isekai.Managers;
using Isekai.StateMachines;
using Isekai.UI.ViewModels.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
    public class PlayerComponentSystem : BaseComponentSystem
    {
        [SerializeField]
        private Animator AnimController;
        private StateMachine m_FSM;

        private void Start()
        {
            m_FSM = new StateMachine();

            var Base = new PlayerBaseState(this);
            PlayerDodgeState DodgeState = new PlayerDodgeState(this);

            m_FSM.AddState("Base", Base);
            m_FSM.AddState("DodgeState", DodgeState);

            m_FSM.AddTransition(
                "Base",
                "DodgeState",
                (transition) => GetSubComponent<MovementComponent>(EComponent.MovementComponent).IsDodging
                );
            m_FSM.AddTransition(
                "DodgeState",
                "Base",
                (transition) => !GetSubComponent<MovementComponent>(EComponent.MovementComponent).IsDodging
                );

            m_FSM.SetStartState("Base");
            m_FSM.Init();
            Debug.Log("fsm initialized");
        }
        private void Update()
        {
            m_FSM.OnLogic();
        }

    }
}

