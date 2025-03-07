using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour
{
    private PlayerInventory playerInventory;

    [SerializeField]
    private Image icon;

    private Item[] itemArray;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        itemArray = playerInventory.itemArray;

        // 처음에 아이콘 오브젝트 꺼줌
        icon.gameObject.SetActive(false);

        playerInventory.ActiveItemSlot += ActiveIcon;
    }

    public void ActiveIcon()
    {
        if (playerInventory.ItemCount > 0)
        {
            if(!icon.gameObject.activeInHierarchy)
                icon.gameObject.SetActive(true);
            SetIcon();
        }
        else
            icon.gameObject.SetActive(false);
    }

    // 인덱스에 맞는 아이콘을 띄워줌
    public void SetIcon()
    {
        icon.sprite = itemArray[playerInventory.CurIndex].itemData.icon;
    }
}
