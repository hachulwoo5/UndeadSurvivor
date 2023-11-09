using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float per;
    public float speed;
    public int maximumRange;

    public bool isFalseObj;
    Rigidbody2D rigid;
    void Awake()
    {
        
            rigid = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
     
        if(isFalseObj==true)
        {
            StartCoroutine(RangeBullet());                           
        }
      
    }
    public void Init(float damage_weapon, float per_weapon, float speed_weapon, Vector3 dir_weapon)        // Received variables from Weapon scripts
    {
        // Weapon ��ũ��Ʈ���� ��������, ���밪 �޾ƿ��� �Ѿ��� ��� v
        this.damage = damage_weapon; 
        this.per = per_weapon;
        this.speed = speed_weapon;
        if (per>-1)  // per�� 0�̻��� �ֵ� �� �Ѿ�, �̷� �ֵ��� �����Ӱ� �ο�
        {
            rigid.velocity = dir_weapon * speed * GameManager.instance.player.bulletSpeed ;     // dir == 1, bullt speed , 15 �κ� ����ȭ �ʿ�
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per ==-1 )                 //  || = or
            return;                                                      // Sword or Not enemy ? Return. Only Gun Weapon

        per--;          // 1 Monster Hit -1 per

        if(per == -1)
        {
            rigid.velocity = Vector2.zero; // Acitve false previously velocity Init
            gameObject.SetActive(false);        // Object pooling 
        }

       
    }
    IEnumerator RangeBullet()
    {
        yield return new WaitForSeconds(2f);
        rigid.velocity = Vector2.zero; // Acitve false previously velocity Init
        gameObject.SetActive(false);        // Object pooling 
    }

}
