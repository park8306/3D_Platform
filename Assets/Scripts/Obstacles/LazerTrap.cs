using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LazerTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject blackPipeTrap;       // 레이저 통과 시 발생할 트랩

    [SerializeField]
    private Transform rayOrigin;            // 반직선을 쏠 위치
    private float rayDistance = 5f;              // 반직선 길이

    private void Update()
    {
        Ray ray = new Ray(rayOrigin.position, Vector3.right);

        if(Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if(hit.transform.CompareTag("Player") && !blackPipeTrap.activeInHierarchy)
            {
                blackPipeTrap.SetActive(true);
            }
        }
        Debug.DrawRay(rayOrigin.position, Vector3.right * rayDistance);
    }
}
