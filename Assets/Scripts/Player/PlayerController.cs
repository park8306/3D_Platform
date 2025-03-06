using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector2 inputDir;       // 입력 받은 방향

    [SerializeField]
    private float moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * inputDir.y + transform.right * inputDir.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // 키를 누르고 있는 상태라면
        if(context.phase == InputActionPhase.Performed)
        {
            inputDir = context.ReadValue<Vector2>();
            Debug.Log(inputDir);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            inputDir = Vector2.zero;
        }
    }
}
