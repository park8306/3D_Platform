using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heal,
    Jump,
    Speed
}


[CreateAssetMenu(menuName = "New Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;     // 아이템 이름
    public string description;  // 아이템 설명
    public ItemType type;   // 아이템 타입
    public float value;     // 타입마다 다르게 적용될 값
    public float duration;  // 지속 시간

    public Sprite icon;
}
