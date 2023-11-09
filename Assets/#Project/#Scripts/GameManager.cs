using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 메모리에 올린다
    [Header("Singleton")]
    public static GameManager instance;
    public Player player;
    public PlayerSkill playerSkill;
    public PoolManager pool;
    public Spawner spawner;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public float EnemyHealthLv;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp= {10, 30, 60 , 100, 150, 210, 280 , 360 ,450,600};

  

    [Header("# Ememy Info")]
    public float power;



    private void Awake()
    {
        instance = this; // 자기 자신을 넣음
        EnemyHealthLv = 1f;
    }

    void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        gameTime += Time.deltaTime;       

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;         
        }
    }

    public void GetExp()  // Exp and Level Manage
    {
        exp++;

            if(exp == nextExp[level])       // nextExp[0] ?= 0 Lv, nextExp[1] ?=  1 Lv
        {
            
            level++;
            EnemyHealthLv += 0.1f;
            exp = 0;
        }

          
    }
}
