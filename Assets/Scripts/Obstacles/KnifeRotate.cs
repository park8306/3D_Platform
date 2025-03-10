using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRotate : MonoBehaviour
{
    private Transform knife; // 돌아갈 칼날

    [SerializeField]
    private float rotateSpeed = 4f;

    private void Start()
    {
        knife = transform.GetChild(0);
    }

    private void Update()
    {
        knife.localEulerAngles += Vector3.up * rotateSpeed *Time.deltaTime;
    }
}
