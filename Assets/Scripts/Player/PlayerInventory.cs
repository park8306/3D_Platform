using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item[] itemArray = new Item[3];

    public void AddItem(Item item)
    {
        // 모든 배열을 순회하면서 비어있는 공간이 있다면 아이템을 넣어준다.
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i] == null)
            {
                itemArray[i] = item;
                item.gameObject.SetActive(false);
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
            }
        }
    }

    // Q를 이용하여 아이템을 사용할 수 있게 구현
}
