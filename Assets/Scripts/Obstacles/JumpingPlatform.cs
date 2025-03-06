using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private float jumpForce = 100f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController != null && animator.GetCurrentAnimatorStateInfo(0).IsName("jumpingPlatform_Idle"))
            {
                animator.SetTrigger("Jumping");
                playerController.JumpingPlatform(jumpForce);
            }
        }
    }
}
