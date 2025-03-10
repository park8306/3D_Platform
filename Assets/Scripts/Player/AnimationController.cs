using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private PlayerController playerController;

    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();

        animator = GetComponent<Animator>();
    }

    public void RunAnimation()
    {
        if (playerController == null) return;

        Vector2 inputDir = playerController.GetInputDir();
        animator.SetFloat("MoveAmount", inputDir.magnitude);
    }

    public void JumpAnimation()
    {
        animator.SetTrigger("Jump");
    }
}
