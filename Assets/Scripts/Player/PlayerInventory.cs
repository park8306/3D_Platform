using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Item[] itemArray = new Item[3];
    public event Action ActiveItemSlot;
    public event Action useItemEvent;

    [SerializeField]
    private int curIndex;
    public int CurIndex { 
        get => curIndex; 
        set { curIndex = value; curIndex %= itemCount; }
    }

    private int itemCount;
    public int ItemCount 
    { 
        get => itemCount; 
        set { itemCount = value; itemCount = Math.Clamp(itemCount, 0, itemArray.Length); } 
    }

    public void AddItem(Item item)
    {
        // 모든 배열을 순회하면서 비어있는 공간이 있다면 아이템을 넣어준다.
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i] == null)
            {
                itemArray[i] = item;
                item.gameObject.SetActive(false);

                ItemCount++;
                ActiveItemSlot?.Invoke();
                return;
            }
        }
    }

    public void UseItem(Item item)
    {
        // 모든 배열을 순회하면서 같은 아이템의 Use 함수를 불러온다.
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i] == item)
            {
                itemArray[i].Use();
                itemArray[i] = null;

                itemCount--;
                useItemEvent?.Invoke();
            }
        }
    }

    // R을 이용하여 아이템 슬롯을 이동시킬 수 있게 구현
    public void ChangeSlot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && itemCount > 0)
        {
            CurIndex++;
            ActiveItemSlot?.Invoke();
        }
    }

    // Q를 이용하여 아이템을 사용할 수 있게 구현
    public void OnUseItem(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && itemCount > 0)
        {
            // 사용한 아이템
            Item useItem = itemArray[curIndex];
            // 아이템 효과 적용
            useItem.Use();
            // 아이템 삭제 후 정렬
            SortItemArray();

            itemCount--;
            CheckItemIndex();
            ActiveItemSlot?.Invoke();
        }
    }

    // 아이템 배열 정렬
    private void SortItemArray()
    {
        itemArray[curIndex] = null;
        if (curIndex != itemArray.Length-1)
        {
            for (int i = curIndex + 1; i < itemArray.Length; i++)
            {
                itemArray[i - 1] = itemArray[i];
                itemArray[i] = null;
            }
        }
    }
    
    // itemArray[curIndex]가 null인지 체크하고 null이면 curIndex를 변경
    private void CheckItemIndex()
    {
        if (itemCount == 0)
        {
            curIndex = 0;
            return;
        }

        if (itemArray[curIndex] == null)
            CurIndex--;
    }
}
