using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();            // Getcomponents In Children , ���� �ڽĵ� ���� �ʱ�ȭ // spawnpoint���� �ν����Ϳ� �Ҵ� ���ص� �� ��ġ���� �� �ʱ�ȭ ����
    }

    void Update()
    {
        timer += Time.deltaTime;        // �� �������� �Һ��� �ð�
        level = Mathf.Min( Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length-1); // int �� ���� �Ҽ��� �Ʒ��� ���� �ݴ�� Ceil 
        // index ������ ���� ���� ��� �� Min �Լ��� �̿��ؼ� ����, �迭�󿡼� ������ ���� ����

        if (timer > spawnData[level].spawnTime)  // 0�����̸� 0�ڸ��� ����Ÿ�� 0.2, 1�����̸� 1�ڸ��� 0.5
        {
            timer = 0;          // 0.2�ʸ��� ��ȯ�ϴ� �Ͱ� ���� 
            Spawn();

            //spawnData[Random.Range(0, 2)]
        }

    }


    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetEnemy(0); // enemy 1���� �������, enemy�� ��ȯ
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;            // spawnPoint(0) �� �ڽĿ�����Ʈ, �ڱ� �ڽ��� ���� ���� 1���� ����      , enemy�� ���� ��ġ�� spawnPoint[1].position �� ����
        // Spawn ���� ���� Here ������ ���ÿ� ���ؿ� �°� �ʱ�ȭ
        enemy.GetComponent<Enemy>().Init(spawnData[level]) ; // 0�����̸� 0���� ������ 4�� �ҷ���, 1�����̸� 1���� ������
       // enemy.GetComponent<Enemy>().Init(spawnData[Random.Range(0, 5)]); // 0�����̸� 0���� ������ 4�� �ҷ���, 1�����̸� 1���� ������


        enemy.transform.parent = GameManager.instance.pool.transform.GetChild(0);
    }

}

[System.Serializable]
public class SpawnData  // ��ȯ �����͸� ����ϴ� Ŭ���� �߰� ����
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public float armor;
    public float damage;

}