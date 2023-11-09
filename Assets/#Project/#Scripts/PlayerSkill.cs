using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;

    public List<string> skillList = new List<string>();
    public List<bool> skillList2 = new List<bool>();
    public bool[] coolDownArray;

    public float VfxSize = 1.2f;
    /*
    [Header("Skill_CoolDown")]
    bool tpCool, SkillColl, Skill2Coll, Skill3Coll;*/

    [Header("Teleport")]
    private float tpDis=1f;
    private float tpCoolTime=1f;

    [Header("Boom")]
    private float BoomCoolTime = 3f;




    public void AcquireSkill(string skillName)
    {
        if (skillList.Count < 5)
        {
            skillList.Add(skillName);
        }

    }

    private void Start()
    {
        player = GetComponent<Player>();
        coolDownArray = new bool[5];

    }

    public void UseSkill(string Key)
    {
        // Space ==1 , Q == 2 , W == 3, E == 4 , R == 5 
        // Ű���� ���� ����Ʈ�� ��� ������ ��ų ����� �ε����� ���� ��ٿ� ����Ʈ �ο�
        // ���⼭ �ε����� ����� ex Space�� ��쿡�� 0�� �迭
        if (Key == "Space")
        {
            SelectSkill(skillList[0], 0); // ù��°�� ���� ��ų�� skilllist[0]�̴� 0��°�� ����ϰ�, 0��° �迭�� ����ϱ���� 
        }
        if (Key == "Q")
        {
            SelectSkill(skillList[1], 1);
        }
        if (Key == "W")
        {
            SelectSkill(skillList[2], 2);
        }
        if (Key == "E")
        {
            SelectSkill(skillList[3], 3);
        }
        if (Key == "R")
        {
            SelectSkill(skillList[4], 4);
        }



    }

    public void SelectSkill(string SkillName, int i)
    {
        // ��ų ���ӿ� ���� ����Ʈ�� ��� ��ų ����
        // �� ��ų�� �ش��ϴ� ��Ÿ���� ���ӿ� ����, ���� ����Ʈ ������ �Ҵ���
        switch (SkillName)
        {
            case "Tp":
                if (!coolDownArray[i])
                    StartCoroutine(Teleport(i));
                break;

            case "Boom":
                if (!coolDownArray[i])
                    StartCoroutine(Boom(i));
                break;
            case "Shot":
                if (!coolDownArray[i])
                    StartCoroutine(Tp2(i));
                break;
            case "Shoot":
                if (!coolDownArray[i])
                    StartCoroutine(Tp3(i));
                break;

        }
    }

    public IEnumerator Teleport(int i)
    {
        GameObject Vfx = GameManager.instance.pool.GetVfx(0);
        Vfx.transform.position = player.transform.position;
        player.transform.Translate(player.inputVec * tpDis);
        StartCoroutine(CheckCooltime(i, tpCoolTime));
        yield return null;
    }

    public IEnumerator Boom(int i)
    {
        GameObject Vfx = GameManager.instance.pool.GetVfx(1);
        Vfx.transform.position = player.transform.position;
        StartCoroutine(CheckCooltime(i, BoomCoolTime));
        yield return null;
    }
    public IEnumerator Tp2(int i)
    {
        GameObject Vfx = GameManager.instance.pool.GetVfx(0);
        Vfx.transform.position = player.transform.position;
        player.transform.Translate(player.inputVec * tpDis * 6);
        StartCoroutine(CheckCooltime(i, 7));
        yield return null;
    }
    public IEnumerator Tp3(int i)
    {
        GameObject Vfx = GameManager.instance.pool.GetVfx(0);
        Vfx.transform.position = player.transform.position;
        player.transform.Translate(player.inputVec * tpDis * 0.5f);
        StartCoroutine(CheckCooltime(i, 0.1f));
        yield return null;
    }








    public IEnumerator CheckCooltime(int Skill_Index, float coolTime)
    {
        coolDownArray[Skill_Index] = true;
        yield return new WaitForSeconds(coolTime);
        coolDownArray[Skill_Index] = false;

    }
}
