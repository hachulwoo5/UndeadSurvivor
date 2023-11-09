using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    // ,, ��������� ������ ����
    public GameObject[] Wepprefabs , Vfxprefabs , Enemyprefabs;
    List<GameObject>[] Weppools, Vfxpools, Enemypools;
   

    void Awake()
    {
        int i = 0;
        Weppools = new List<GameObject>[Wepprefabs.Length]; // ������ �迭�� ũ��� �����ؾ���, ���� �迭�� �ʱ�ȭ �ϰ� ������ ����Ʈ �³״� �ʱ�ȭ�� �ȵ�
        Vfxpools = new List<GameObject>[Vfxprefabs.Length];
        Enemypools = new List<GameObject>[Enemyprefabs.Length];

        // ����Ʈ���� �ʱ�ȭ
        for (i =0; i<Weppools.Length;i++)
        {
            Weppools[i] = new List<GameObject>(); // ������ ����Ʈ�� ��ȸ�ϸ鼭 �ʱ�ȭ
        }
        for (i = 0; i < Vfxpools.Length; i++)
        {
            Vfxpools[i] = new List<GameObject>();
        }
        for (i = 0; i < Enemypools.Length; i++)
        {
            Enemypools[i] = new List<GameObject>(); // ������ ����Ʈ�� ��ȸ�ϸ鼭 �ʱ�ȭ
        }


    }

    #region ���� ���� �ֿ� ����

    
    public GameObject GetWep(int index) // ���� ������Ʈ�� ��ȯ�ϴ� �Լ� , ������ ������Ʈ�� ������ �����ϴ� �Ű����� index �߰� ex Get(0)
    {
        GameObject select = null; // Get(0) = select�� 0�� ���͸� �ְ� return�ؼ� ��ȯ��

        // ... ������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ ����
        foreach(GameObject item in Weppools[index])        // �迭 ����Ʈ���� �����͸� ���������� �����ϴ� �ݺ���, ���⼭ ������ ���� ���� ��������(Gameobject item)�� ��������
        {
            if(!item.activeSelf)        // ��Ȱ��ȭ �� �� ã��
            {
                // ... �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... �� ���� �ִٸ�?
      
        if (!select)
        {
            // ... ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(Wepprefabs[index], transform); // transform == ���ڽ� poolManager�� ����, select��  prefabs���� �ϳ� ������ ���� ����
            Weppools[index].Add(select);                                                               // ���õ� select�� pools �迭�� ���� ����
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

        // ��ã������ ?
        // ���Ӱ� ���� �� select�� �Ҵ�
        if (!select)
        {
            select = Instantiate(Vfxprefabs[index], transform.GetChild(2));
            Vfxpools[index].Add(select);
        }


        // ���������� select ���� / ��Ȱ�� ã�Ұų�, ������ �߰ų�
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
