using Cysharp.Threading.Tasks;
using Isekai.Components;
using Isekai.Datas;
using Isekai.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Systems
{
   
    public class HandleSpriteWhenMoveSystem : BaseSystem
    {
        private MovementComponent m_moveComponent;
        private CombatComponent m_combatComponent;

        public Transform m_sprite;
        private Vector3 m_originalScale;

        public override void Initialize()
        {
            Debug.Log(DebugHelper.LINE);
            Debug.Log("Initializing HandleSpriteWhenMoveComponent");
            m_originalScale = m_sprite.localScale;

            m_moveComponent = GetSubComponent<MovementComponent>(EComponent.MovementComponent);
            m_combatComponent = GetSubComponent<CombatComponent>(EComponent.CombatComponent);

            Debug.Log("HandleSpriteWhenMoveComponent Initialized");
            Debug.Log(DebugHelper.LINE);
        }


        void Update()
        {
            HandleMove();
        }
        void HandleMove()
        {
            if (m_combatComponent!=null&&!m_combatComponent.IsAttacking)
            {
                if (m_moveComponent.LastMoveDirection.x > 0)
                {
                    m_sprite.transform.localScale = new Vector3(m_originalScale.x, m_originalScale.y, m_originalScale.z);
                }
                else if (m_moveComponent.LastMoveDirection.x < 0)
                {
                    m_sprite.transform.localScale = new Vector3(-m_originalScale.x, m_originalScale.y, m_originalScale.z);
                }
            }

        }
        public override void OnRemove()
        {
        }

        public override void BindComponentProperty()
        {
            m_combatComponent.PropertyValueChanged += HandleCombatComponentValueChanged;
        }
        public override void UnbindComponentProperty()
        {
            m_combatComponent.PropertyValueChanged -= HandleCombatComponentValueChanged;
        }

        void HandleCombatComponentValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_combatComponent.AttackMoveDirection):
                    if (m_combatComponent.AttackMoveDirection.x > 0)
                    {
                        m_sprite.transform.localScale = new Vector3(m_originalScale.x, m_originalScale.y, m_originalScale.z);
                    }
                    else
                    {
                        m_sprite.transform.localScale = new Vector3(-m_originalScale.x, m_originalScale.y, m_originalScale.z);
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

