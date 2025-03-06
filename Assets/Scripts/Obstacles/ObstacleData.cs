using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public string displayName;
    public string description;

    public int damage;
}
