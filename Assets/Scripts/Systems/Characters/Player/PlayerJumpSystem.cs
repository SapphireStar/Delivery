using Cysharp.Threading.Tasks;
using Isekai.Components;
using Isekai.Datas;
using Isekai.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerJumpSystem : BaseSystem
{
    private MovementComponent m_movementComponent;
    Rigidbody2D m_rigidbody;
    private IPlayerData m_data;
    public override void Initialize()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
       m_data = (IPlayerData)GetComponent<IComponentSystem>().Data;
        m_movementComponent = GetSubComponent<MovementComponent>(Isekai.Factories.EComponent.MovementComponent);
    }
    public void OnProcessJump(CallbackContext context)
    {
        if (context.performed&&m_movementComponent.IsGrounded)
        {
            Debug.Log("jump");
            m_movementComponent.IsJumping = true;
            m_rigidbody.velocity += Vector2.up * m_data.JumpHeight;
        }

    }
    public override void BindComponentProperty()
    {
        m_movementComponent.PropertyValueChanged += HandleMovementComponentPropertyChanged;
    }
    public override void UnbindComponentProperty()
    {
        m_movementComponent.PropertyValueChanged -= HandleMovementComponentPropertyChanged;
    }
    void HandleMovementComponentPropertyChanged(object sender, PropertyValueChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(m_movementComponent.IsGrounded):
                m_movementComponent.IsJumping = false;
                break;
            default:
                break;
        }
    }

    public async override UniTaskVoid BindComponentPropertyAsync()
    {

    }



    public override void OnRemove()
    {
    }


}
