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
        // 캐스팅 시작 위치 ,원의 반지름, 쏘는 방향, 캐스팅 길이 , 대상 레이어  // 플레이어 주변에만 있을 거라 쏘진 않음
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0,targetLayer);
        nearestTarget = Getnearest(); // Getnearest 는 Transform값 result를 반환해서 nearestTarget에다 집어 넣음
    }
    Transform Getnearest()
    {
        Transform result = null;
        float diff = 100;

        // 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 교체, diff는 계속 변함 curdiff도 계속 계산 결국 최솟값 찾음
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
            // foreach를 통해 10개의 유닛들을 모두 검색, diff는 계속 지정되며 결국 가장 작은 diff가 됐을 때 result로 선정
        }
        return result ;
    }
}
