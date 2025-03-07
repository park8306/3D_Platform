using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Item
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Use()
    {
        playerStat.Heal((int)itemData.value);
    }
}
