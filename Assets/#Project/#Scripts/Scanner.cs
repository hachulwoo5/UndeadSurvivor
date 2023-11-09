using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;       // targetlayer = enemy
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()

    {
        // ĳ���� ���� ��ġ ,���� ������, ��� ����, ĳ���� ���� , ��� ���̾�  // �÷��̾� �ֺ����� ���� �Ŷ� ���� ����
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0,targetLayer);
        nearestTarget = Getnearest(); // Getnearest �� Transform�� result�� ��ȯ�ؼ� nearestTarget���� ���� ����
    }
    Transform Getnearest()
    {
        Transform result = null;
        float diff = 100;

        // �ݺ����� ���� ������ �Ÿ��� ����� �Ÿ����� ������ ��ü, diff�� ��� ���� curdiff�� ��� ��� �ᱹ �ּڰ� ã��
        foreach (RaycastHit2D target in targets)    
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff<diff)
            {
                diff = curDiff;
                result = target.transform;
            }
            // foreach�� ���� 10���� ���ֵ��� ��� �˻�, diff�� ��� �����Ǹ� �ᱹ ���� ���� diff�� ���� �� result�� ����
        }
        return result ;
    }
}
