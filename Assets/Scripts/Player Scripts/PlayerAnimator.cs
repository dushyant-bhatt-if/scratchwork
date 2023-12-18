using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;

    private void Update()
    {
        animator.SetFloat("BlendX",playerMovement.currentVel.x * 100);
        animator.SetFloat("BlendY", playerMovement.currentVel.z* 100);
    }
}
