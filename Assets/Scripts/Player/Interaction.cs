using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private float interactionRate = 0.1f; // 조사 주기
    private float lastTime;     // 마지막으로 측정한 시간

    [SerializeField]
    private float maxDistance;  // 반직선의 최대 거리

    public LayerMask itemLayer;

    private Item item;
    private ItemData itemData;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // interactionRate의 시간마다 ray 확인
        if(Time.time - lastTime > interactionRate)
        {
            lastTime = Time.time;
            RaycastHit hit;
            
            // 화면의 정중앙으로부터 반직선을 쏜다.
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            // 만약 반직선에 닿은 장애물이 있다면
            if (Physics.Raycast(ray, out hit, maxDistance, itemLayer))
            {
                if (hit.collider.GetComponent<Item>() == null) return;

                item = hit.collider.GetComponent<Item>();
                itemData = item.itemData;

                // 화면에 이름과 설명 띄워주기
                if (itemData != null)
                {
                    UIManager.Instance.ShowItemInfo(itemData);
                }
            }
            else
            {
                UIManager.Instance.DisableItemInfo();
                item = null;
                itemData = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        Gizmos.DrawRay(ray);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && item != null)
        {
            playerInventory.AddItem(item);
        }
    }
}
