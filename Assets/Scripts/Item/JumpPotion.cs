using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPotion : Item
{
    protected override void Start()
    {
        base.Start();

    }
    public override void Use()
    {
        playerStat.JumpForceCo = playerStat.StartCoroutine(playerStat.IncreaseJumpCount(itemData));
    }
}
