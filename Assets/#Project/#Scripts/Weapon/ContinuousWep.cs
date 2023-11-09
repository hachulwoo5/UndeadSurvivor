using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContinuousWep: MonoBehaviour
{
    CircleCollider2D circleCollider2D;

    public float time;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        StartCoroutine(ToggleCollider(time));

    }
   
    private IEnumerator ToggleCollider(float ctime)
    {
        while (true)
        {
            // 콜라이더를 비활성화하고 일정 시간을 기다린 후 다시 활성화합니다.
            circleCollider2D.enabled = false;
            yield return new WaitForSeconds(ctime);
            circleCollider2D.enabled = true;
            yield return new WaitForSeconds(ctime);
        }
    }
}