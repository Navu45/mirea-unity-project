using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    [field: SerializeField]
    public int Damage
    {
        get; protected set;
    }
    [SerializeField] private Animator animator;
    [SerializeField] private float attackTime;

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private IDisposable chaseTarget;
    private IDisposable fight;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 3;
    }

    public void Return()
    {
        agent.destination = startPosition;
        Fight(false);
    }

    public void Fight(bool value = true)
    {
        animator.SetBool("Attack", value);
    }

    private void SetSpeed(int speed)
    {
        animator.SetInteger("Run", speed);
    }

    protected override void SetTarget(Unit target)
    {
        this.target = target;

        if (chaseTarget != null)
        {
            chaseTarget.Dispose();
            chaseTarget = null;
        }

        chaseTarget = Observable.EveryUpdate()
            .TakeWhile(_ => target != null)
            .TakeWhile(_ => !NoHP)
            .Finally(() => {

                chaseTarget = null;
                if (NoHP)
                {
                    Die();
                }
            })
            .Subscribe(_ =>
            {
                agent.destination = target.transform.position;
                if (agent.remainingDistance > 4)
                {
                    ChaseTarget();

                }
                else
                {
                    AttackTarget();
                }
            });
    }

    private void Die()
    {
        agent.ResetPath();
        animator.SetTrigger("Death");
    }

    private void ChaseTarget()
    {
        SetSpeed(1);
        Fight(false);
    }

    private void AttackTarget()
    {
        if (fight == null)
        {
            SetSpeed(0);
            Fight();
            fight = Observable.Interval(TimeSpan.FromSeconds(attackTime))
                .Finally(() => fight = null)
                .TakeWhile(_ => !NoHP && agent.remainingDistance > 4).Subscribe(_ =>
                {
                    transform.LookAt(target.transform);
                    MakeDamage(Damage);
                });
        }
    }
}
