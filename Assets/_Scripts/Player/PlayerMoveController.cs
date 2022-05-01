using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMoveController : MonoBehaviour
{
    private PlayerMove player;
    private PlayerAnimationController animation;
    private PlayerInput playerInput;
    private Vector3 input;
    private Vector3 zero = Vector3.zero;

    IDisposable stopDis;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMove>();
        animation = GetComponent<PlayerAnimationController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>();
        if (input != zero)
        {
            HandleInput();
            animation.SetAnimation(AnimType.Run);
        }
        else if (stopDis == null && !player.IsStopped)
        {
            animation.SetAnimation(AnimType.Idle);
            stopDis = Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                player.Stop();
                stopDis.Dispose();
                stopDis = null;
            });
        }
    }

    private void HandleInput()
    {
        input = -ConvertInputToDirection(input);
        player.Move(input);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(input), 0.01f);
    }

    private Vector3 ConvertInputToDirection(Vector3 vector)
    {
        Vector3 vec = vector;
        vec.z = -vector.x;
        vec.x = vector.y;
        return vec;
    }
}
