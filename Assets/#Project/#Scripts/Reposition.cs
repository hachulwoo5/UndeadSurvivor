using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Reposition : MonoBehaviour
{
    Collider2D coll;


    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);  // dir = 1 or -1 > horizontal move
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);  // dir = 1 or -1 > vertical move
                }
                else if (diffX == diffY)  // 대각선 이동
                {
                    transform.Translate((Vector3.right * dirX + Vector3.up * dirY) * 40);
                }
                break;

            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 30 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }



}
