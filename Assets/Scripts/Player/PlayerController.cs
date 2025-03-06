using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector2 inputDir;       // 입력 받은 방향
    private Vector2 mouseDelta;

    private float mouseSensitivity = 0.1f;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float rayDistance = 0.1f;

    public LayerMask groundLayer;

    [SerializeField]
    private Transform cameraContainerTr;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (cameraContainerTr == null)
            cameraContainerTr = transform.GetChild(0);
    }

    private void LateUpdate()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Look()
    {
        // 마우스의 변화량을 바탕으로 회전
        float deltaX = -mouseDelta.y * mouseSensitivity;
        float deltaY = mouseDelta.x * mouseSensitivity;

        // 카메라를 가지고 있는 부모 transform을 회전 시킴
        cameraContainerTr.localEulerAngles += new Vector3(deltaX, 0, 0);

        transform.eulerAngles += new Vector3(0, deltaY, 0);
    }

    private void Move()
    {
        // 캐릭터의 앞쪽 방향과 오른쪽 방향을 이용하여 rigidbody의 velocity 값을 변경 시켜준다.
        Vector3 dir = transform.forward * inputDir.y + transform.right * inputDir.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    private bool IsGround()
    {
        // 중심점과 떨어진 거리
        float distance = GetComponent<CapsuleCollider>().radius;

        // 중심을 기준으로 콜라이더의 radius 만큼의 떨어진 ray를 4개 생성
        Ray[] rays = { 
            new Ray(transform.position + (transform.forward * distance) + (transform.up * 0.01f), Vector3.down  ),
            new Ray(transform.position + (-transform.forward * distance) + (transform.up * 0.01f), Vector3.down  ),
            new Ray(transform.position + (transform.right * distance) + (transform.up * 0.01f), Vector3.down  ),
            new Ray(transform.position + (-transform.right * distance) + (transform.up * 0.01f), Vector3.down)
        };

        // 모든 레이들을 순회하면서 땅과 닿는지 확인
        for (int i = 0; i < rays.Length; i++)
        {
            // 하나라도 땅과 닿아있으면 true 반환
            if (Physics.Raycast(rays[i], rayDistance, groundLayer))
            {
                return true;
            }
        }

        return false;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        // 키를 누르고 있는 상태라면
        if(context.phase == InputActionPhase.Performed)
        {
            inputDir = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            inputDir = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // 점프 버튼을 눌렀다면 점프 실행
        if(context.phase == InputActionPhase.Started && IsGround())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        Debug.Log(mouseDelta);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray[] ray = {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down  ),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down  ),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down  ),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };
        for (int i = 0; i < ray.Length; i++)
        {
            Gizmos.DrawRay(ray[i].origin, Vector3.down * rayDistance);
        }
    }
}
