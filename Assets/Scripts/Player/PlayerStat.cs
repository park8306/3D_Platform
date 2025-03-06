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

    private void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        UIManager.Instance.SetHPUI((float)hp / maxHP);
    }
}
