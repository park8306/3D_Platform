using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector2 inputDir;       // 입력 받은 방향
    private Vector2 mouseDelta;

    private float mouseSensitivity = 0.1f;
    private float camCurXRot;

    private PlayerStat playerStat;

    [SerializeField]
    private float rayDistance = 0.1f;

    public LayerMask groundLayer;

    [SerializeField]
    private Transform cameraContainerTr;

    private float minXRot = -85f;
    private float maxXRot = 85f;

    [SerializeField]
    private Transform thirdPersonCameraContainerTr;

    private float thirdMinXRot = -60f;
    private float thirdMaxXRot = 15f;

    private AnimationController animationController;
    private Transform characterTr;

    private bool isThirdPersonView = false; // 3인칭 시점인지 확인

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerStat = GetComponent<PlayerStat>();

        animationController = GetComponentInChildren<AnimationController>(true);
        animationController.gameObject.SetActive(false);
        characterTr = animationController.transform;

        if (cameraContainerTr == null)
            cameraContainerTr = transform.GetChild(0);

        thirdPersonCameraContainerTr.gameObject.SetActive(false);
        cameraContainerTr.gameObject.SetActive(true);
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
        camCurXRot += mouseDelta.y * mouseSensitivity;

        float deltaY;
        if (isThirdPersonView)
        {
            // 3인칭 카메라
            camCurXRot = Mathf.Clamp(camCurXRot, thirdMinXRot, thirdMaxXRot);
            deltaY = thirdPersonCameraContainerTr.localEulerAngles.y + mouseDelta.x * mouseSensitivity;
            thirdPersonCameraContainerTr.localEulerAngles = new Vector3(-camCurXRot, deltaY, 0);
        }
        else
        {
            //1인칭 카메라
            camCurXRot = Mathf.Clamp(camCurXRot, minXRot, maxXRot);

            deltaY = mouseDelta.x * mouseSensitivity;
            //카메라를 가지고 있는 부모 transform을 회전 시킴
            cameraContainerTr.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

            transform.eulerAngles += new Vector3(0, deltaY, 0);
        }
    }

    private void Move()
    {

        Vector3 dir;
        if(isThirdPersonView)
        {
            // 3인칭 카메라
            dir = thirdPersonCameraContainerTr.forward * inputDir.y + thirdPersonCameraContainerTr.right * inputDir.x;
        }
        else
        {
            // 1인칭 카메라
            dir = cameraContainerTr.forward * inputDir.y + transform.right * inputDir.x;
        }

        // 캐릭터의 앞쪽 방향과 오른쪽 방향을 이용하여 rigidbody의 velocity 값을 변경 시켜준다.
        dir *= playerStat.moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;

        if(inputDir.magnitude > 0 && isThirdPersonView)
        {
            dir.Normalize();
            Vector3 lookDir = new Vector3(dir.x, 0, dir.z);
            characterTr.forward = lookDir;
        }

        if (isThirdPersonView)
            animationController.RunAnimation();
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
                playerStat.remainJumpCount = playerStat.maxJumpCount;
                Debug.Log(playerStat.remainJumpCount);
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
            Jump();
        }
        else if(context.phase == InputActionPhase.Started && playerStat.remainJumpCount != 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        _rigidbody.AddForce(Vector3.up * playerStat.jumpForce, ForceMode.Impulse);
        playerStat.remainJumpCount--;

        if(isThirdPersonView)
            animationController.JumpAnimation();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnChangeView(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            ChangeView();
        }
    }

    // 시점 변경 설정
    private void ChangeView()
    {
        isThirdPersonView = !isThirdPersonView;

        thirdPersonCameraContainerTr.gameObject.SetActive(isThirdPersonView);
        cameraContainerTr.gameObject.SetActive(!isThirdPersonView);
        animationController.gameObject.SetActive(isThirdPersonView);
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

    public void JumpingPlatform(float jumpForce)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public Vector2 GetInputDir()
    {
        return inputDir;
    }
}
