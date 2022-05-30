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
    private IDisposable stopMovement;
    private IDisposable spellCasting;

    void Start()
    {
        player = GetComponent<Player>();
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
        MoveForward();
        RotateWithVector();
        TurnPlayerWithAnim();
        CastSpell();
    }

    private void CastSpell()
    {
        for (int i = 0; i < player.spellCount; i++)
        {
            if (player.spellCasted[i] && spellCasting == null)
            {
                SpellAnimation(player.spellNames[i]);
                spellCasting = Observable.Timer(TimeSpan.FromSeconds(player.spellContext.spells[i].ReloadTime))
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
        if (player.IsInputEmpty() && stopMovement == null)
        {
            stopMovement = Observable.Timer(TimeSpan.FromSeconds(0.1f)).Subscribe(_ => animator.SetFloat("Speed", 0));
        }
        else if (player.IsMoving())
        {
            stopMovement?.Dispose();
            stopMovement = null;
            animator.SetFloat("Speed", player.InputVector.y);
        }
    }

    private void RotateWithVector()
    {
        if (player.InputVector.x != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, player.RotationTowards(), angularSpeed * Time.deltaTime);
        }
    }

    private void TurnPlayerWithAnim()
    {
        if (player.InputVector.y == 0)
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
