using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "WeaponStat")]
public class WeaponStat : ScriptableObject
{
    public string Name;
    public int weaponLv;
    public float damage;
    public float count;
    public float cool;
    public float speed;
    public float size;
    public int per;
    public enum WepType
    {
        Range,Melee
    }
    public WepType wepType;
}
