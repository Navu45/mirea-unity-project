using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;    

    private void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.03f);
    }
}
