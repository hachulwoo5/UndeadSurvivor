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
            // �ݶ��̴��� ��Ȱ��ȭ�ϰ� ���� �ð��� ��ٸ� �� �ٽ� Ȱ��ȭ�մϴ�.
            circleCollider2D.enabled = false;
            yield return new WaitForSeconds(ctime);
            circleCollider2D.enabled = true;
            yield return new WaitForSeconds(ctime);
        }
    }
}