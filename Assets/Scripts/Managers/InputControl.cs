using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : Singleton<InputControl>
{
    public PlayerInput InputSystem = new PlayerInput();
    public PlayerInput.OnFootActions OnFoot;

    public InputControl()
    {
        OnFoot = InputSystem.OnFoot;
    }
    public void EnableNormalMovement()
    {
        OnFoot.Enable();
    }
    public void DisableNormalMovement()
    {
        OnFoot.Disable();
    }
}
