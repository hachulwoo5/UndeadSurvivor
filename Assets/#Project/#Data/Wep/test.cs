using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float distanceX = 5f;
        float distanceY = -5f;
        float speed = 15f;

        float distance = Mathf.Abs(distanceX) + Mathf.Abs(distanceY);
        float time = distance / speed;

        Debug.Log("�������� �ɸ��� �ð�: " + time + "��");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
