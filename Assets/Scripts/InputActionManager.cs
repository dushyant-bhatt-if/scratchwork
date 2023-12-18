using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager : MonoBehaviour
{
    public CharInput asset;

    public Vector2 Move;
    public Vector2 Look;
    public float Jump;
    public float Sprint;
    public float Interact;

    public void Awake()
    {
        asset = new CharInput();
        asset.Enable();

        Move = asset.Player.Move.ReadValue<Vector2>();
        Look = asset.Player.Look.ReadValue<Vector2>();

    }

    public void Update()
    {

        Move = asset.Player.Move.ReadValue<Vector2>();
        Look = asset.Player.Look.ReadValue<Vector2>();

    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        Jump = ctx.ReadValue<float>();
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        Sprint = ctx.ReadValue<float>();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        Interact = ctx.ReadValue<float>();
    }
}
  