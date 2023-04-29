using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Components
{
    public class CombatComponent : BaseComponent
    {
        private bool m_isDead = false;
        public bool IsDead
        {
            get
            {
                return m_isDead;
            }
            set
            {
                ChangePropertyAndNotify(ref m_isDead, value);
            }
        }
        private bool m_canAttack;
        public bool CanAttack
        {
            get => m_canAttack;
            set
            {
                ChangePropertyAndNotify(ref m_canAttack, value);
            }
        }
        private bool m_isAttacking;
        public bool IsAttacking
        {
            get => m_isAttacking;
            set
            {
                ChangePropertyAndNotify(ref m_isAttacking, value);
            }
        }
        private float m_primaryBuffer;
        public float PrimaryBuffer
        {
            get => m_primaryBuffer;
            set
            {
                ChangePropertyAndNotify(ref m_primaryBuffer, value);
            }
        }

        private Vector2 m_attackDirection;
        public Vector2 AttackMoveDirection
        {
            get => m_attackDirection;
            set
            {
                ChangePropertyAndNotify(ref m_attackDirection, value);
            }
        }

        public override void Initialize(BaseComponentSystem system)
        {
        }
    }

}
