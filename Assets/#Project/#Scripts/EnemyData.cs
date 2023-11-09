using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public int attackDamage;
    // �߰����� �� ������ �ʿ信 ���� ������ �� �ֽ��ϴ�.
}