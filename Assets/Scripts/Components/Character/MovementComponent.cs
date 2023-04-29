using Isekai.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Isekai.Components
{

    public class MovementComponent:BaseComponent
    {
        private Rigidbody2D m_rigidbody;
        private bool m_isMoving;
        public bool IsMoving
        {
            get
            {
                return m_isMoving;
            }
            set
            {
                ChangePropertyAndNotify(ref m_isMoving, value);
            }
        }

        private bool m_canDodge = true;
        public bool CanDodge
        {
            get => m_canDodge;
            set
            {
                ChangePropertyAndNotify(ref m_canDodge, value);
            }
        }

        private bool m_isDodging;
        public bool IsDodging
        {
            get
            {
                return m_isDodging;
            }
            set
            {
                ChangePropertyAndNotify(ref m_isDodging, value);
            }
        }
        private bool m_isJumping;
        public bool IsJumping
        {
            get
            {
                return m_isJumping;
            }
            set
            {
                ChangePropertyAndNotify(ref m_isJumping, value);
            }
        }
        private bool m_inAir;
        public bool InAir
        {
            get
            {
                return m_inAir;
            }
            set
            {
                ChangePropertyAndNotify(ref m_inAir, value);
            }
        }
        private bool m_isGrounded;
        public bool IsGrounded
        {
            get
            {
                return m_isGrounded;
            }
            set
            {
                ChangePropertyAndNotify(ref m_isGrounded, value);
            }
        }
        private Vector2 m_lastMoveDirection;
        public Vector2 LastMoveDirection
        {
            get
            {
                return m_lastMoveDirection;
            }
            set
            {
                ChangePropertyAndNotify(ref m_lastMoveDirection, value);
            }
        }
        public override void Initialize(BaseComponentSystem system)
        {
            m_rigidbody = system.GetComponent<Rigidbody2D>();
        }
        public void SetVelocity(Vector2 velocity)
        {
            m_rigidbody.velocity = new Vector2(velocity.x,m_rigidbody.velocity.y);

            if (velocity != Vector2.zero)
            {
                LastMoveDirection = new Vector2(velocity.x,0);
            }

            ChangePropertyAndNotify(m_rigidbody.velocity, new Vector2(velocity.x, m_rigidbody.velocity.y));
        }

        public Vector2 GetVelocity()
        {
            return m_rigidbody.velocity;
        }


    }

}
