using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;
    public GameManager gameMgr;

    private void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchInput.performed += ctx => TouchPerformed(ctx);
    }

    private void TouchPerformed(InputAction.CallbackContext context)
    {
        gameMgr.NextContact(touchControls.Touch.TouchPos.ReadValue<Vector2>(), true);
    }
}
