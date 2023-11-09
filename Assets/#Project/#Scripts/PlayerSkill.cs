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
        // 키별로 숫자 리스트에 담긴 네임의 스킬 실행과 인덱스에 따른 쿨다운 리스트 부여
        // 여기서 인덱스를 배분함 ex Space의 경우에는 0번 배열
        if (Key == "Space")
        {
            SelectSkill(skillList[0], 0); // 첫번째로 먹은 스킬은 skilllist[0]이니 0번째를 사용하고, 0번째 배열을 사용하기로함 
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
        // 스킬 네임에 따라 리스트에 담긴 스킬 실행
        // 각 스킬에 해당하는 쿨타임을 네임에 따른, 같은 리스트 순번에 할당함
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
