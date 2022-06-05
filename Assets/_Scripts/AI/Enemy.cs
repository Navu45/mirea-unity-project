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
    [Header("Attack")]
    [SerializeField] private float attackTime;
    [SerializeField] private int attackDistance;

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private IDisposable chaseTarget;
    private IDisposable fight;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
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

    public override void SetTarget(Unit target)
    {
        if (NoHP)
        {
            return;
        }

        if (chaseTarget != null)
        {
            chaseTarget.Dispose();
            chaseTarget = null;
        }

        this.target = target;
        chaseTarget = Observable.EveryUpdate()
            .TakeWhile(_ => target != null)
            .TakeWhile(_ => !NoHP)
            .Finally(() => {

                chaseTarget = null;
                chaseTarget?.Dispose();
                if (NoHP)
                {
                    Die();
                    target.target = null;
                }
            })
            .Subscribe(_ =>
            {
                agent.destination = target.transform.position;
                transform.LookAt(target.transform);
                if (agent.remainingDistance > attackDistance)
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
        Battlefield.EnemyCount--;
    }

    private void ChaseTarget()
    {
        SetSpeed(1);
        Fight(false);
    }

    private void AttackTarget()
    {
        SetSpeed(0);
        Fight();
        if (fight == null)
        {
            fight = Observable.Interval(TimeSpan.FromSeconds(attackTime - .5f))
                .Finally(() => fight = null)
                .TakeWhile(_ => !NoHP && agent.remainingDistance <= attackDistance).Subscribe(_ =>
                {
                    MakeDamage(Damage);
                });
        }
    }
}
