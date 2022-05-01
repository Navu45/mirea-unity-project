using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public AnimType CurrentAnim => (AnimType) animator.GetFloat("Speed");

    public void SetAnimation(AnimType animType)
    {
        animator.SetFloat("Speed", (float) animType);
    }
}

public enum AnimType
{
    RunToStop = -1,
    Idle = 0,
    SlowRun = 1,
    Run = 2
}
