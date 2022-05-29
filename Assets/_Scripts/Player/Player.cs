using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerStats))]
public class Player : MonoBehaviour
{
    [Header("Abilities")]
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public PlayerStats playerStats;
    public Enemy target;
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

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
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
    public Quaternion RotationTowards() => Quaternion.LookRotation(Mathf.Sign(InputVector.y) * MoveDirection);
    public bool IsMoving() => InputVector.y != 0;
    public bool IsInputEmpty() => InputVector == zeroVector;
    public bool TryGetTarget(Spell spellInfo)
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out RaycastHit hit, spellInfo.distance, enemyVolumeMask))
        {
            if (hit.transform.TryGetComponent(out TargetController targets))
            {
                return target = targets.GetRandomEnemy();
            }
        }
        return target = null;
    }
}
