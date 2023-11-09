using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public int attackDamage;
    // 추가적인 적 정보를 필요에 따라 포함할 수 있습니다.
}