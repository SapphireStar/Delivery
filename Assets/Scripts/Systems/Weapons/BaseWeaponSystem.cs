using Cysharp.Threading.Tasks;
using Isekai.Components;
using Isekai.Datas;
using Isekai.Events;
using Isekai.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Isekai.Systems
{
    public class BaseWeaponSystem : BaseSystem
    {
        private BaseWeaponData m_weaponData;

        private CombatComponent m_combatComponent;
        private MovementComponent m_movementComponent;

        protected Animator m_baseAnimator;
        protected Animator m_weaponAnimator;

        private int m_attackCounter;
        private Vector2 m_tempAtkDirection;
        private bool m_attackAnimationComplete;

        [SerializeField]
        private LayerMask m_attackLayer;

        public override void Initialize()
        {
            m_weaponData = GetComponent<IComponentSystem>().Data as BaseWeaponData;
            if (!GetComponentInChildren<SwordAnimationEventEmitter>())
            {
                Debug.LogError("Can't find animation event emitter in children, please add and config an animation event emitter");
            }

            m_baseAnimator = transform.Find("Base").GetComponent<Animator>();
            m_weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        }
        public virtual void InitializeWeapon(CombatComponent combatComponent, MovementComponent movementComponent)
        {
            m_combatComponent = combatComponent;
            m_movementComponent = movementComponent;
        }
        public virtual void EnterWeapon()
        {
            if (!m_combatComponent.CanAttack)
            {
                return;
            }
            m_attackAnimationComplete = false;
            m_combatComponent.IsAttacking = true;

            m_baseAnimator.SetBool("Attacking", true);
            m_weaponAnimator.SetBool("Attacking", true);

            if (m_attackCounter >= m_weaponData.movementSpeed.Length)
            {
                m_attackCounter = 0;
            }

            m_baseAnimator.SetInteger("AttackCounter", m_attackCounter);
            m_weaponAnimator.SetInteger("AttackCounter", m_attackCounter);
        }

        public virtual void ExitWeapon()
        {
            
            m_baseAnimator.SetBool("Attacking", false);
            m_weaponAnimator.SetBool("Attacking", false);

            m_attackCounter++;
        }
        public virtual void OnProcessAttack(CallbackContext context)
        {
            Vector3 mousePos = UtilClass.GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            //预留玩家输入的新的攻击方向，等当前攻击动画播放完成后再切换攻击方向
            m_tempAtkDirection = (mousePos - transform.position).normalized;
            //m_animator.SetLayerWeight((int)PlayerAnimLayer.AttackLayer, 1);
            m_combatComponent.PrimaryBuffer = m_weaponData.CombatBuffer;
        }

        protected virtual void checkBuffer()
        {
            if (!m_combatComponent.CanAttack)
            {
                return;
            }

            if (m_combatComponent.PrimaryBuffer > 0)
            {
                m_combatComponent.PrimaryBuffer -= Time.deltaTime;
                //等待当前攻击动画播放完成后切换攻击方向
                if (!m_combatComponent.IsAttacking)
                {
                    m_combatComponent.IsAttacking = true;
                }
            }
            else if (m_combatComponent.PrimaryBuffer < 0)
            {
                if (m_attackAnimationComplete)
                {
                    m_attackCounter = 0;
                }
                if (m_attackAnimationComplete && m_combatComponent.IsAttacking)
                {    
                    m_combatComponent.IsAttacking = false;
                    m_attackAnimationComplete = false;
                }    
            }
        }
        protected void checkAttackDirection()
        {
            if (!m_combatComponent.IsAttacking)
            {
                if (m_movementComponent.LastMoveDirection.x > 0)
                {
                    m_combatComponent.AttackMoveDirection = Vector2.right;
                }
                else if (m_movementComponent.LastMoveDirection.x < 0)
                {
                    m_combatComponent.AttackMoveDirection = Vector2.left;
                }
            }
        }

        protected virtual float calculateWeaponDamage()
        {
            return m_weaponData.WeaponDamage;
        }
        private void Update()
        {
            checkBuffer();
            checkAttackDirection();
        }


        #region AnimationTriggers
        private List<IDamagable> m_attackTargets = new List<IDamagable>();
        public virtual void OnAttackStart()
        {
            m_attackTargets.Clear();
            var results = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) + 
                                                  m_combatComponent.AttackMoveDirection.normalized, 
                                                  new Vector2(3, 2),0,m_attackLayer);
            foreach (var item in results)
            {
                IDamagable target = item.GetComponent<IDamagable>();

                if (target!=null&&!m_attackTargets.Contains(target))
                {
                    m_attackTargets.Add(target);
                    target.ApplyDamage(calculateWeaponDamage());
                }
            }
        }
        public virtual void OnAttackStop()
        {
            var results = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) +
                                                  m_combatComponent.AttackMoveDirection.normalized,
                                                  new Vector2(3, 2), 0, m_attackLayer);
            foreach (var item in results)
            {
                IDamagable target = item.GetComponent<IDamagable>();

                if (target != null && !m_attackTargets.Contains(target))
                {
                    m_attackTargets.Add(target);
                    target.ApplyDamage(calculateWeaponDamage());
                }
            }
        }

        public virtual void OnAttackAnimationStartMove()
        {
            var dir = m_combatComponent.AttackMoveDirection.x > 0 ? Vector2.right : Vector2.left;
            m_movementComponent.SetVelocity(dir.normalized * m_weaponData.movementSpeed[m_attackCounter]);
        }
        public virtual void OnAttackAnimationStopMove()
        {
            m_movementComponent.SetVelocity(Vector2.zero);
        }
        public virtual void OnAttackAnimationComplete()
        {
            if (m_movementComponent.LastMoveDirection.x > 0)
            {
                m_combatComponent.AttackMoveDirection = Vector2.right;
            }
            else if (m_movementComponent.LastMoveDirection.x < 0)
            {
                m_combatComponent.AttackMoveDirection = Vector2.left;
            }
            m_combatComponent.IsAttacking = false;
            m_attackAnimationComplete = true;
        }
        #endregion


        public override void OnRemove()
        {
            
        }
        //这里使用异步的方式来绑定事件，因为绑定事件和初始化Component的时机相同，因此需要在Component初始化完成后再绑定事件
        public async override UniTaskVoid BindComponentPropertyAsync()
        {
            while (m_combatComponent == null)
            {
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            m_combatComponent.PropertyValueChanged += HandleCombatComponentValueChanged;
        }
        public override void BindComponentProperty()
        {

        }
        public override void UnbindComponentProperty()
        {
            m_combatComponent.PropertyValueChanged -= HandleCombatComponentValueChanged;
        }

        void HandleCombatComponentValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_combatComponent.CanAttack):
                    m_attackCounter = 0;
                    if (!m_combatComponent.CanAttack)
                    {
                        m_combatComponent.PrimaryBuffer = 0;
                        ExitWeapon();
                    }
                    break;
                default:
                    break;
            }
        }


    }
}

