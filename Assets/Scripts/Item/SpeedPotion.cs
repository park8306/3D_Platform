using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Item
{
    protected override void Start()
    {
        base.Start();

    }
    public override void Use()
    {
        playerStat.MoveSpeedCo = playerStat.StartCoroutine(playerStat.IncreaseMoveSpeed(itemData));
    }
}
