using Cysharp.Threading.Tasks;
using FSM;
using Isekai.Components;
using Isekai.Datas;
using Isekai.Factories;
using Isekai.StateMachines;
using Isekai.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Isekai.Systems
{

    public class PlayerCombatSystem : BaseSystem
    {
        private IPlayerData m_playerData;

        //Components
        private CombatComponent m_combatComponent;
        private MovementComponent m_movementComponent;

        [SerializeField]
        private BaseWeaponSystem m_weaponSystem;

        private Animator m_animator;
        private PlayerInput.OnFootActions m_onFoot;
        private float m_meleeBuffer;
        private bool canAttack = true;

        //StateMachines
        FSM.StateMachine fsm;
        public PlayerAttackState PrimaryAttackState { get; private set; }
        public PlayerAttackState SecondaryAttackState { get; private set; }

        private Vector2 m_tempAtkDirection;
        public override void Initialize()
        {
            m_combatComponent = GetSubComponent<CombatComponent>(EComponent.CombatComponent);
            m_movementComponent = GetSubComponent<MovementComponent>(EComponent.MovementComponent);
            m_animator = GetSubComponent<AnimatorComponent>(EComponent.AnimatorComponent).AnimController;

            m_playerData = (IPlayerData)GetComponent<IComponentSystem>().Data;
            
            initializeInput();
            initializeWeapons();
            initializeStateMachine();
        }
        void initializeInput()
        {
            InputControl.Instance.EnableNormalMovement();

            m_onFoot = InputControl.Instance.OnFoot;

            //m_onFoot.Attack.performed += ProcessAttack;
        }
       
        void initializeWeapons()
        {
            m_weaponSystem.InitializeWeapon(m_combatComponent,m_movementComponent);
        }

        void initializeStateMachine()
        {
            fsm = new StateMachine();
            
            PrimaryAttackState = new PlayerAttackState(m_weaponSystem);
            SecondaryAttackState = new PlayerAttackState(m_weaponSystem);

            fsm.AddState("Base", new State(
            onLogic: (state) =>
                {
                }
            ));
            fsm.AddState("PrimaryAttack", PrimaryAttackState);

            fsm.AddTransition(
                "Base",
                "PrimaryAttack",
                (transition) => m_combatComponent.IsAttacking
                );

            fsm.AddTransition(
                "PrimaryAttack",
                "Base",
                (transition) => !m_combatComponent.IsAttacking
                );

            fsm.SetStartState("Base");
            fsm.Init();
        }
        void Update()
        {
            fsm.OnLogic();
        }
        public void OnPrimaryAttackInput(CallbackContext context)
        {
            if (context.started)
            {
                ProcessPrimaryAttack(context);
            }
        }
        void ProcessPrimaryAttack(CallbackContext context)
        {
            if (canAttack)
            {
                m_weaponSystem.OnProcessAttack(context);
            }
        }
        public void OnSecondaryAttackInput(CallbackContext context)
        {
            if (context.started)
            {

            }
        }


        public override void OnRemove()
        {
            
        }
        public override void BindComponentProperty()
        {
            m_movementComponent.PropertyValueChanged += HandleMovementComponentValueChanged;
            m_combatComponent.PropertyValueChanged += HandleCombatComponentValueChanged;
        }
        public override void UnbindComponentProperty()
        {
            m_movementComponent.PropertyValueChanged -= HandleMovementComponentValueChanged;
            m_combatComponent.PropertyValueChanged -= HandleCombatComponentValueChanged;
        }

        void HandleMovementComponentValueChanged(object sender, PropertyValueChangedEventArgs e) 
        {
            switch (e.PropertyName)
            {
                case nameof(m_movementComponent.IsDodging):
                    if (m_movementComponent.IsDodging)
                    {
                        canAttack = false;
                        m_animator.SetBool("Attacking", false);
                        m_combatComponent.IsAttacking = false;
                        //m_animator.SetLayerWeight((int)PlayerAnimLayer.AttackLayer, 0);
                    }
                    else
                    {
                        canAttack = true;
                    }
                    break;
                default:
                    break;
            }
        }

        void HandleCombatComponentValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_combatComponent.AttackMoveDirection):
                    //if (m_combatComponent.AttackDirection.x > 0)
                    //{
                    //    m_movementComponent.LastMoveDirection = Vector2.right;
                    //}
                    //else if(m_combatComponent.AttackDirection.x <0)
                    //{
                    //    m_movementComponent.LastMoveDirection = Vector2.left;
                    //}
                    break;
                case nameof(m_combatComponent.IsAttacking):
                    if (m_combatComponent.IsAttacking)
                    {
                        m_animator.SetBool("Running", false);
                        m_animator.SetBool("Attacking", true);
                    }
                    else
                    {
                        m_animator.SetBool("Attacking", false);
                    }
                    break;
                default:
                    break;
            }
        }



        public async override UniTaskVoid BindComponentPropertyAsync()
        {
        }
    }
}


