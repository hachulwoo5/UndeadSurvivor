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
        spawnPoint = GetComponentsInChildren<Transform>();            // Getcomponents In Children , 하위 자식들 전부 초기화 // spawnpoint들을 인스펙터에 할당 안해도 됨 위치값을 다 초기화 했음
    }

    void Update()
    {
        timer += Time.deltaTime;        // 한 프레임이 소비한 시간
        level = Mathf.Min( Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length-1); // int 로 변형 소수점 아래는 버림 반대는 Ceil 
        // index 에러는 레벨 변수 계산 시 Min 함수를 이용해서 막음, 배열상에서 오버가 되지 않음

        if (timer > spawnData[level].spawnTime)  // 0레벨이면 0자리의 스폰타임 0.2, 1레벨이면 1자리의 0.5
        {
            timer = 0;          // 0.2초마다 소환하는 것과 같음 
            Spawn();

            //spawnData[Random.Range(0, 2)]
        }

    }


    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetEnemy(0); // enemy 1개만 들어있음, enemy를 소환
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;            // spawnPoint(0) 는 자식오브젝트, 자기 자신을 빼기 위해 1부터 시작      , enemy의 생성 위치는 spawnPoint[1].position 과 같음
        // Spawn 몬스터 수준 Here 생성과 동시에 수준에 맞게 초기화
        enemy.GetComponent<Enemy>().Init(spawnData[level]) ; // 0레벨이면 0몬스터 정보값 4개 불러옴, 1레벨이면 1몬스터 정보값
       // enemy.GetComponent<Enemy>().Init(spawnData[Random.Range(0, 5)]); // 0레벨이면 0몬스터 정보값 4개 불러옴, 1레벨이면 1몬스터 정보값


        enemy.transform.parent = GameManager.instance.pool.transform.GetChild(0);
    }

}

[System.Serializable]
public class SpawnData  // 소환 데이터를 담당하는 클래스 추가 생성
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public float armor;
    public float damage;

}