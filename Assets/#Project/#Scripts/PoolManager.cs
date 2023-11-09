using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    // ,, 프리펩들을 보관한 변수
    public GameObject[] Wepprefabs , Vfxprefabs , Enemyprefabs;
    List<GameObject>[] Weppools, Vfxpools, Enemypools;
   

    void Awake()
    {
        int i = 0;
        Weppools = new List<GameObject>[Wepprefabs.Length]; // 프리팹 배열의 크기와 같이해야함, 담을 배열만 초기화 하고 각각의 리스트 걔네는 초기화가 안됨
        Vfxpools = new List<GameObject>[Vfxprefabs.Length];
        Enemypools = new List<GameObject>[Enemyprefabs.Length];

        // 리스트마저 초기화
        for (i =0; i<Weppools.Length;i++)
        {
            Weppools[i] = new List<GameObject>(); // 각각의 리스트들 순회하면서 초기화
        }
        for (i = 0; i < Vfxpools.Length; i++)
        {
            Vfxpools[i] = new List<GameObject>();
        }
        for (i = 0; i < Enemypools.Length; i++)
        {
            Enemypools[i] = new List<GameObject>(); // 각각의 리스트들 순회하면서 초기화
        }


    }

    #region 몬스터 생성 주요 구문

    
    public GameObject GetWep(int index) // 게임 오브젝트를 반환하는 함수 , 가져올 오브젝트를 종류를 결정하는 매개변수 index 추가 ex Get(0)
    {
        GameObject select = null; // Get(0) = select에 0번 몬스터를 넣고 return해서 소환함

        // ... 선택한 풀의 비활성화된 게임오브젝트 접근
        foreach(GameObject item in Weppools[index])        // 배열 리스트들의 데이터를 순차적으로 접근하는 반복문, 여기서 나오는 값은 따로 지역변수(Gameobject item)로 담아줘야함
        {
            if(!item.activeSelf)        // 비활성화 된 거 찾음
            {
                // ... 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... 다 쓰고 있다면?
      
        if (!select)
        {
            // ... 새롭게 생성하고 select 변수에 할당
            select = Instantiate(Wepprefabs[index], transform); // transform == 내자신 poolManager에 넣음, select에  prefabs에서 하나 가져온 것을 넣음
            Weppools[index].Add(select);                                                               // 선택된 select를 pools 배열에 집어 넣음
        }

        return select;
    }
    #endregion

    public GameObject GetVfx(int index)
    {
        GameObject select = null;

        foreach (GameObject item in Vfxpools[index])
            if (!item.activeSelf)
            {

                select = item;
                select.SetActive(true);
                break;
            }

        // 못찾았으면 ?
        // 새롭게 생성 후 select에 할당
        if (!select)
        {
            select = Instantiate(Vfxprefabs[index], transform.GetChild(2));
            Vfxpools[index].Add(select);
        }


        // 최종적으로 select 배출 / 비활을 찾았거나, 생성을 했거나
        return select;
    }

    public GameObject GetEnemy(int index) 
    {
        GameObject select = null; 

        foreach (GameObject item in Enemypools[index])        
        {
            if (!item.activeSelf)         
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
      

        if (!select)
        {
            select = Instantiate(Enemyprefabs[index], transform);
            Enemypools[index].Add(select);                                                              
        }

        return select;
    }
}
