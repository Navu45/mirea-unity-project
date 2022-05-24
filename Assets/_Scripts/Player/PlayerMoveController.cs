using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public float angularSpeed = 2;

    private Player player;
    private IDisposable stopPlayer;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void SetSpeed(AnimType animType)
    {
        animator.SetFloat("Speed", (float) animType);
    }

    public void RotateWithAnimation(float value)
    {
        animator.SetFloat("Turn", value);
    }

    private void Update()
    {
        TurnPlayerWithAnim();
        MoveForward();
        RotateWithVector();
    }

    private void MoveForward()
    {
        if (player.IsInputEmpty() && animator.GetFloat("Speed") != 0)
        {
            stopPlayer = Observable.Timer(TimeSpan.FromSeconds(0.1f)).Subscribe(_ => SetSpeed(AnimType.Idle));
        }
        else if (player.IsMoving() || animator.GetFloat("Speed") != 0 && player.InputVector.x != 0)
        {
            stopPlayer?.Dispose();
            SetSpeed(AnimType.Run);
        }
    }

    private void RotateWithVector()
    {
        if (animator.GetFloat("Speed") == 2 && player.InputVector.x != 0 || !player.IsInputEmpty() && !player.IsTurning180())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, player.RotationTowards(), angularSpeed * Time.deltaTime);
        }
    }

    private void TurnPlayerWithAnim()
    {
        if (player.InputVector.y != 0 && player.IsTurning180())
        {
            RotateWithAnimation(180);
        }
        else
        {
            RotateWithAnimation(player.InputVector.x * 90);
        }
    }
}

public enum AnimType
{
    RunToStop = -1,
    Idle = 0,
    SlowRun = 1,
    Run = 2
}
