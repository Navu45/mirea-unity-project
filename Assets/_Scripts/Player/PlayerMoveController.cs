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
            input = ConvertInputToDirection(input);
            HandleInput();
        }
        else if (stopDis == null && !player.IsStopped)
        {
            animation.SetSpeed(AnimType.Idle);
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
        animation.SetSpeed(AnimType.Run);
        float angleBefore = transform.eulerAngles.y;
        player.Move(input);
        float angleAfter = transform.eulerAngles.y;
        animation.Turn(angleAfter - angleBefore);
    }

    private Vector3 ConvertInputToDirection(Vector3 vector)
    {
        Vector3 vec = vector;
        vec.z = vector.y;
        vec.x = vector.x;
        vec = Quaternion.AngleAxis(-135, transform.up) * vec;
        return vec;
    }
}
