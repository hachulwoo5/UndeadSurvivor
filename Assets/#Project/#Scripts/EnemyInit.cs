using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInit : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.transform.position = GameManager.instance.spawner.spawnPoint[Random.Range(1, GameManager.instance.spawner.spawnPoint.Length)].position;
        }
    }


}
