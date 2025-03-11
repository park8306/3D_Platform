using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private int maxHP = 100;
    [SerializeField]
    private int hp = 100;
    public int HP { 
        get => hp;
        set 
        { 
            hp = value; 
            hp = Mathf.Clamp(hp, 0, maxHP); 
        }
    }

    private float originMoveSpeed;
    public float moveSpeed;
    private float originjumpForce;
    public float jumpForce;
    public int maxJumpCount;
    public int remainJumpCount;

    public Coroutine JumpForceCo;
    public Coroutine MoveSpeedCo;

    private void Start()
    {
        originMoveSpeed = 5f;
        originjumpForce = 4f;

        moveSpeed = originMoveSpeed;
        jumpForce = originjumpForce;

        maxJumpCount = 1;
        remainJumpCount = maxJumpCount;

        hp = maxHP;
    }

    // 데미지 입음
    public void TakeDamage(int damage)
    {
        HP -= damage;
        UIManager.Instance.SetHPUI((float)hp / maxHP);

        if(HP <= 0)
        {
            UIManager.Instance.DefeatUIActive();
        }
    }

    // 체력 회복
    public void Heal(int value)
    {
        HP += value;
        UIManager.Instance.SetHPUI((float)hp / maxHP);
    }

    public IEnumerator IncreaseJumpCount(ItemData jumpItem)
    {
        maxJumpCount += (int)jumpItem.value;

        yield return new WaitForSeconds(jumpItem.duration);

        maxJumpCount -= (int)jumpItem.value;
    }
    public IEnumerator IncreaseMoveSpeed(ItemData speedItem)
    {
        moveSpeed += speedItem.value;

        yield return new WaitForSeconds(speedItem.duration);

        moveSpeed = originMoveSpeed;
    }
}
