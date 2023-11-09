using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyStat")]
public class EnemyStat : ScriptableObject
{
    public string Name;
    public int Lv;
    public float damage;
    public float speed;
    public int armor;
    public float health;
    public float Maxhelath;

    public enum EnemyType
    {
        Normal,MiddleBoss,Boss
    }
    public EnemyType enemyType;
}
