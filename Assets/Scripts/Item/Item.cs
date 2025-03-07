using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected PlayerStat playerStat;

    public ItemData itemData;

    protected virtual void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
    }

    public abstract void Use();

}
