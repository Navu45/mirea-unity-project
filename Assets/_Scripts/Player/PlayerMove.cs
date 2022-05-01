using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public float speed = 5;
    public float acceleration = 3;

    public bool IsStopped => agent.hasPath;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = acceleration;
    }

    public void Move(Vector3 moveVector)
    {
        agent.isStopped = false;
        agent.SetDestination(transform.position + moveVector);        
    }

    public void Stop()
    {
        agent.ResetPath();
    }

}
