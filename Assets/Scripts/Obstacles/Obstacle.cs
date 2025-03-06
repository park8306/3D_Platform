using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleData obstacleData;

    private PlayerStat playerStat = null;

    private Coroutine hitCoroutine = null;

    // 플레이어가 장애물 안에 들어온 경우 데미지를 줄 수 있도록 playerStat에 저장해둠
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerStat = other.GetComponent<PlayerStat>();

            if (playerStat != null && hitCoroutine == null)
            {
                hitCoroutine = StartCoroutine(Hit());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStat = null;

            if(hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
                hitCoroutine = null;
            }
        }
    }

    public IEnumerator Hit()
    {
        while (true)
        {
            playerStat.TakeDamage(obstacleData.damage);

            Debug.Log(playerStat.HP);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
