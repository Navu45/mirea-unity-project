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
    private IDisposable movement;
    private IDisposable spellCasting;

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

    public void SpellAnimation(string name)
    {
        animator.SetTrigger(name);
    }

    private void Update()
    {
        TurnPlayerWithAnim();
        MoveForward();
        RotateWithVector();
        CastSpell();
    }

    private void CastSpell()
    {
        for (int i = 0; i < player.spellCount; i++)
        {
            if (player.spellCasted[i] && spellCasting == null)
            {
                SpellAnimation(player.spellNames[i]);
                spellCasting = Observable.Timer(TimeSpan.FromSeconds(player.spellContext.spells[i].duration))
                    .Subscribe(_ =>
                    {
                        spellCasting?.Dispose();
                        spellCasting = null;
                    });
            }
        }
    }

    private void MoveForward()
    {
        if (player.IsInputEmpty() && animator.GetFloat("Speed") != 0)
        {
            movement = Observable.Timer(TimeSpan.FromSeconds(0.1f)).Subscribe(_ => SetSpeed(AnimType.Idle));
        }
        else if (player.IsMoving() || animator.GetFloat("Speed") != 0 && player.InputVector.x != 0)
        {
            movement?.Dispose();
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
