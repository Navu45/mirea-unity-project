using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerInput PlayerInput;

    public Vector2 zeroVector = Vector3.zero;
    public Vector2 moveVector = Vector3.zero;

    void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        InitInputForFrame();
    }

    public Vector2 InputVector {
        get => moveVector;
    }
    public void InitInputForFrame() => moveVector = PlayerInput.actions["Move"].ReadValue<Vector2>();
    public Vector3 MoveDirection => transform.forward * InputVector.y + transform.right * InputVector.x;
    public bool IsTurning180() =>  InputVector != zeroVector && Quaternion.Angle(transform.rotation, RotationTowards()) == 180;    
    public Quaternion RotationTowards() => Quaternion.LookRotation(MoveDirection);
    public bool IsMoving() => InputVector.y != 0;
    public bool IsInputEmpty() => InputVector == zeroVector;
}
