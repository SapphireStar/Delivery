using Cysharp.Threading.Tasks;
using Isekai.Components;
using Isekai.Datas;
using Isekai.Factories;
using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Isekai.Systems
{
    public class PlayerMoveSystem : BaseSystem, IMovementSystem
    {
        private IPlayerData m_playerData;

        //Components
        private MovementComponent m_movementComponent;
        private CombatComponent m_combatComponent;



        //temp values
        private Vector3 m_velocity;
        private Vector2 m_direction;
        private float m_dodgeSpeed;
        private float m_minimumDodgeSpeed;
        private Vector2 m_lastDirection = Vector2.right;
        private Animator m_animator;
        private PlayerInput.OnFootActions m_onFoot;

        public override void Initialize()
        {
            Debug.Log(DebugHelper.LINE);
            Debug.Log("Initializing Player Move System");
            m_playerData = (IPlayerData)GetComponent<IComponentSystem>().Data;

            m_movementComponent = GetSubComponent<MovementComponent>(EComponent.MovementComponent);
            m_combatComponent = GetSubComponent<CombatComponent>(EComponent.CombatComponent);

            var animatorComponent = GetSubComponent<AnimatorComponent>(EComponent.AnimatorComponent);
            if (animatorComponent == null)
            {
                Debug.Log("ComponentSystem has no AnimatorComponent, animation control will be non functional");
            }
            else
            {
                m_animator = animatorComponent.AnimController;
            }
           
            Debug.Log("Player Move System Initialized");
            Debug.Log(DebugHelper.LINE);
        }

        void Update()
        {
            checkGrounded();
            ProcessMove(m_direction);
            HandleJumpAnim();
        }
        public void OnMoveInput(CallbackContext context)
        {
            m_direction = new Vector2(context.ReadValue<Vector2>().x,0);
        }
        public void ProcessMove(Vector2 direction)
        {
            if (!m_movementComponent.IsDodging)
            {
                m_velocity = m_direction * m_playerData.Speed;
                m_movementComponent.SetVelocity(m_velocity);
                if (m_velocity != Vector3.zero)
                {
                    m_animator?.SetBool("Running", true);
                }
                else
                {
                    m_animator?.SetBool("Running", false);
                }
            }
            else if (m_direction != Vector2.zero)//当角色不能移动时，依然记录LastMoveDirection，以便玩家在可以移动前提前选择动作方向
            {
                m_movementComponent.LastMoveDirection = m_direction;   
            }
        }
        public void OnDodgeInput(CallbackContext context)
        {
            if (m_movementComponent.CanDodge)
            {
                m_movementComponent.IsDodging = true;
                m_dodgeSpeed = m_playerData.DodgeSpeed;
                m_minimumDodgeSpeed = m_playerData.MinimumDodgeSpeed;
                m_animator?.SetBool("Dodging", true);
                m_animator?.SetTrigger("Roll");   
                dodgeSpeedDrop().Forget();
            }
        }

        //递减当前翻滚速度
        async UniTaskVoid dodgeSpeedDrop()
        {
            while (m_dodgeSpeed > m_minimumDodgeSpeed)
            {
                float dodgeSpeedDropMultiplier = 5f;
                m_dodgeSpeed -= m_dodgeSpeed * dodgeSpeedDropMultiplier * Time.deltaTime;
                m_movementComponent.SetVelocity(m_lastDirection.normalized * m_dodgeSpeed);
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }   
            m_dodgeSpeed = 0;
        }
        public void OnDodgeAnimationComplete()
        {
            m_animator?.SetBool("Dodging", false);
            m_movementComponent.IsDodging = false;
        }

        [SerializeField]
        private float groundCheckOffsetX;
        [SerializeField]
        private float groundCheckOffsetY;
        [SerializeField]
        private float groundCheckDistance;
        [SerializeField]
        private LayerMask groundCheckLayer;
        private void checkGrounded()
        {

                var hit = Physics2D.Raycast(transform.position + new Vector3(groundCheckOffsetX, groundCheckOffsetY, 0),
                Vector2.right, groundCheckDistance, groundCheckLayer);
                if (hit.collider != null)
                {
                    m_movementComponent.IsGrounded = true;
                    m_movementComponent.InAir = false;
                }
                else
                {
                    m_movementComponent.IsGrounded = false;
                    m_movementComponent.InAir = true;
                }
        }
        private void OnDrawGizmos()
        {
                Gizmos.DrawLine(transform.position + new Vector3(groundCheckOffsetX, groundCheckOffsetY,0),
                                    transform.position + new Vector3(groundCheckOffsetX, groundCheckOffsetY, 0)+Vector3.right*groundCheckDistance);
            
        }
        private void HandleJumpAnim()
        {
            if (m_movementComponent.InAir&&!m_movementComponent.IsDodging)
            {
                if (m_movementComponent.GetVelocity().y > 0)
                {
                    m_animator.SetBool("JumpUp", true);
                    m_animator.SetBool("JumpDown", false);
                    m_animator.SetBool("Running", false);
                    m_animator.SetBool("Dodging", false);
                }
                else if (m_movementComponent.GetVelocity().y < 0)
                {
                    m_animator.SetBool("JumpUp", false);
                    m_animator.SetBool("JumpDown", true);
                    m_animator.SetBool("Running", false);
                    m_animator.SetBool("Dodging", false);
                }
            }
            else
            {
                m_animator.SetBool("JumpUp", false);
                m_animator.SetBool("JumpDown", false);
            }
        }

        public override void BindComponentProperty()
        {
            if (m_movementComponent != null)
            {
                m_movementComponent.PropertyValueChanged += HandleMovementComponentValueChanged;
            }
            
        }
        public override void UnbindComponentProperty()
        {
            m_movementComponent.PropertyValueChanged -= HandleMovementComponentValueChanged;
        }

        void HandleMovementComponentValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_movementComponent.LastMoveDirection):
                    m_lastDirection = m_movementComponent.LastMoveDirection;
                    break;
                case nameof(m_movementComponent.IsDodging):
                    if (m_movementComponent.IsDodging)
                    {

                        m_movementComponent.CanDodge = false;
                    }
                    else
                    {
                        m_movementComponent.CanDodge = true;
                    }
                    break;
                default:
                    break;
            }
        }

        void HandleMeleeComponentValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_combatComponent.IsAttacking):
                    if (m_combatComponent.IsAttacking)
                    {
                        m_movementComponent.SetVelocity(Vector2.zero);
                    }
                    break;
                default:
                    break;
            }
        }
        public override void OnRemove()
        {
        }

        public async override UniTaskVoid BindComponentPropertyAsync()
        {
            
        }
    }

}
