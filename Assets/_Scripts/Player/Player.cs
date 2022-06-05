using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;
using UniRx;

[RequireComponent(typeof(PlayerInput))]
public class Player : Unit
{
    [Header("Abilities")]
    [HideInInspector] public PlayerInput playerInput;

    public SpellContext spellContext;
    public readonly string[] spellNames = { "Lightning", "Electricity" };
    public TargetController currentBattlefield;

    [Header("Input")]
    public Vector2 zeroVector = Vector3.zero;
    public Vector2 moveVector = Vector3.zero;
    public int spellCount = 2;
    public bool[] spellCasted = { false, false };
    public bool[] mouse = new bool[] { false, false };
    public LayerMask enemyVolumeMask;

    public bool TryDecreaseManaLevel(float cost, float duration)
    {
        if ((stats as PlayerStats).Mana - cost < 0)
        {
            return false;
        }

        Observable.Interval(TimeSpan.FromSeconds(.1f))
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(duration)))
            .Subscribe(m =>
            {
                (stats as PlayerStats).Mana -= cost / 10;
            });

        return true;
    }

    public bool SpellCasted(int i) => spellCasted[i] && (spellContext.spells[i].target == Target.None || target != null);
    protected override void Start()
    {
        base.Start();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        InitInputForFrame();
    }

    public Vector2 InputVector {
        get => moveVector;
    }
    public void InitInputForFrame() 
    {
        moveVector = playerInput.actions["Move"].ReadValue<Vector2>();
        spellCasted[0] = Keyboard.current.digit1Key.ReadValue() == 1;
        spellCasted[1] = Keyboard.current.digit2Key.ReadValue() == 1;
        mouse[0] = Mouse.current.leftButton.ReadValue() == 1;
        mouse[0] = Mouse.current.rightButton.ReadValue() == 1;
    }

    public Vector3 MoveDirection => transform.forward * InputVector.y + transform.right * InputVector.x;
    public bool IsTurning180() =>  InputVector != zeroVector && Quaternion.Angle(transform.rotation, RotationTowards()) == 180;    
    public Quaternion RotationTowards() => Quaternion.LookRotation(MoveDirection);
    public bool IsMoving() => InputVector.y != 0;
    public bool IsInputEmpty() => InputVector == zeroVector;
    public bool TryGetTarget(Spell spellInfo)
    {
        if (target && !target.NoHP)
        {
            return true;
        }

        if (spellInfo.target == Target.Enemy)
        {
            if (currentBattlefield)
            {
                target = currentBattlefield.GetRandomEnemy();
                return target.NoHP ? target = null : target;
            }
            Ray ray = new Ray(transform.position + Vector3.up * 2, transform.TransformDirection(Vector3.forward));
            if (Physics.Raycast(ray, out RaycastHit hit, spellInfo.distance, enemyVolumeMask))
            {
                if (hit.transform.TryGetComponent(out TargetController targets))
                {
                    target = targets.GetRandomEnemy();
                    return target.NoHP ? target = null : target;
                }
            }
        }
        else if (spellInfo.spawnPoint == Target.Player)
        {
            target = null;
            return true;
        }
        return target = null;
    }

    public override void SetTarget(Unit target) {}
}
