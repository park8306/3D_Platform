using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] pos;        // 플랫폼이 움직일 위치

    private int startIndex;
    private int goalIndex;

    private void Start()
    {
        StartCoroutine(RepeatMove());

        startIndex = 0;
        goalIndex = 1;
    }

    private IEnumerator RepeatMove()
    {
        float lerpTime = 0;
        float goalTime = 3f;
        while (true)
        {
            lerpTime += Time.deltaTime;

            transform.position = Vector3.Lerp(pos[startIndex].position, pos[goalIndex].position, lerpTime / goalTime);

            if(lerpTime > goalTime)
            {
                lerpTime = 0;
                int temp = startIndex;
                startIndex = goalIndex;
                goalIndex = temp;

                yield return new WaitForSeconds(goalTime);
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
