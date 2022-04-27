using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    private PlayerMove player;
    private PlayerAnimationController animationController;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMove>();
        animationController = GetComponent<PlayerAnimationController>();
        playerInput = GetComponent<PlayerInput>();

        // Have it run your code when the Action is triggered.
        playerInput.actions["Move"].performed += (ctx) =>
        {
            Move(ctx);
            animationController.SetAnimation(AnimType.Run);
        };

        playerInput.actions["Move"].canceled += (ctx) =>
        {
            animationController.SetAnimation(AnimType.Idle);
        };

        // Start listening for control changes.
        playerInput.actions["Move"].Enable();
    }

    void OnDisable()
    {
        playerInput.actions["Move"].Disable();
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        player.Move(ctx.ReadValue<Vector2>());
    }
}
