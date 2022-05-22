using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.AxisState;

public class InputAxisProvider : MonoBehaviour, IInputAxisProvider
{
    [SerializeField] private PlayerInput playerInput;
    public float GetAxisValue(int axis)
    {
        Vector3 axisValue = playerInput.actions["Move"].ReadValue<Vector2>();
        return axis == 0 ? axisValue.x : axis == 1 ? axisValue.y : axisValue.z;
    }
}
