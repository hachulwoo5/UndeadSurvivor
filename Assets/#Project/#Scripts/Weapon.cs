using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string id;
    public int prefabId;
    public WeaponStat weaponStat;

    [HideInInspector]
    public float Initdamage;
    [HideInInspector]
    public float Initcount;
    [HideInInspector]
    public float Initspeed;
    [HideInInspector]
    public float Initsize;
    [HideInInspector]
    public float Initper;
    [HideInInspector]
    public float Initcool;
    

    public string weaponName;
    public float damage;
    public float count;
    public float per;
    public float cool;
    public float speed;
    public float size ;
    public int Lv;
    public int weaponlevel;
    public string weapontype;
    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
        Init();

    }
    void Start()
    {
    }
    void Update()
    {
        ActWep();

    }
    /// <summary>
    /// 무기 추가 설정
    /// 1. 함수 생성
    /// 2. Act 등록
    /// 3. WeaponCheck 설정
    /// 4. Bullet, Weapon 스크립트 등록
    /// 5. Data 제작 및 작성 후 삽입
    /// </summary>

    
    public void Init()          // Start game, This figure designation
    {
        weaponName = weaponStat.Name;
        weaponlevel = weaponStat.weaponLv;
        weapontype = weaponStat.wepType.ToString();

        damage = weaponStat.damage;
        count = weaponStat.count;
        cool = weaponStat.cool;
        size = weaponStat.size;
        per = weaponStat.per;
        speed = weaponStat.speed;

       

        Initdamage = damage;
        Initcount = count;
        Initsize = size;
        Initcool = cool;
        Initper = per;
        Initspeed = speed;
    }

    /// <summary>
    /// 플레이어 간섭 damage, speed  제외 damage 는 enemy에서 speed 는 bullet에서 관여 
    /// 회전 speed , 갯수 count, 크기 size, 쿨타임 cool
    /// </summary>
    void Sword()        
    {          
        for (int index = 0; index < count; index++)  
        {
            Transform sword;

            if (index < transform.childCount)
            {
                sword = transform.GetChild(index); // 가지고 있다면 풀 매니저에서 꺼내오지 않고 내꺼 쓰겠다
            }
            else // 2개 있는데 2개가 넘었다?
            {
                sword = GameManager.instance.pool.GetWep(prefabId).transform; 
                sword.parent = transform; 
            }
            Vector3 rotVec = Vector3.forward * 360 * index / count;          

            sword.localPosition = Vector3.zero;       
            sword.localRotation = Quaternion.identity; 

            sword.Rotate(rotVec);                                  
            sword.Translate(sword.up * 1.5f, Space.World);         
            sword.transform.localScale = new Vector3(size * player.WeaponSize, size * player.WeaponSize, size * player.WeaponSize);
            sword.GetComponent<Bullet>().Init(damage, per, speed, Vector3.zero);            
        }
    }
    /// <summary>
    /// 플레이어 간섭 damage, speed  제외 damage 는 enemy에서 speed 는 bullet에서 관여 
    /// 회전 speed , 갯수 count, 크기 size, 쿨타임 cool
    /// Fire : cool, size, 
    /// </summary>
    void Fire()
    {
        if (!player.scanner.nearestTarget)            // 플레이어의 스캐너 스크립트 속 변수에 접근
            return; // filter

        Vector3 targetPos = player.scanner.nearestTarget.position;      // Location
        Vector3 dir = targetPos - transform.position;                   // Direction
        dir = dir.normalized;                                           // Direction Maintenance, Size = 1

        Transform fire = GameManager.instance.pool.GetWep(prefabId).transform;

        fire.transform.parent = transform;
        fire.position = transform.position;
        fire.rotation = Quaternion.FromToRotation(Vector3.up, dir);           
        fire.transform.localScale = new Vector3(size * player.WeaponSize, size * player.WeaponSize, size * player.WeaponSize);
        fire.GetComponent<Bullet>().Init(damage, per,speed, dir);

    }                             // here damage and count getting it in inspector and delivery to Bullet scripts
    /// <summary>
    /// 플레이어 간섭 damage, speed  제외 damage 는 enemy에서 speed 는 bullet에서 관여 
    /// 회전 speed , 갯수 count, 크기 size, 쿨타임 cool
    /// Boom : Size,    Cool(Act에서 관리)
    /// </summary>
    IEnumerator Boom()
    {
        
        GameObject boom = GameManager.instance.pool.GetWep(prefabId);
        boom.transform.position = player.transform.position;
        boom.transform.parent = transform;
        boom.transform.localScale = new Vector3(size * GameManager.instance.player.WeaponSize, size * GameManager.instance.player.WeaponSize, size * GameManager.instance.player.WeaponSize);
        boom.GetComponent<CircleCollider2D>().enabled = true;
        boom.GetComponent<Bullet>().Init(damage, per, speed,Vector3.zero);
        yield return new WaitForSeconds(0.1f);

        boom.GetComponent<CircleCollider2D>().enabled = false;
        

    }
    /// <summary>
    /// 플레이어 간섭 damage, speed  제외 damage 는 enemy에서 speed 는 bullet에서 관여 
    /// 회전 speed , 갯수 count, 크기 size, 쿨타임 cool
    /// count, size,    cool(Act에서관리)
    /// </summary>
    void Crossfire()
    {
        float counting = count + GameManager.instance.player.WeaponCount*2;
        float angleStep = 360f / counting; // 무기 간의 각도 간격 계산

        for (int index = 0; index < counting; index++)
        {
            GameObject crossfire = GameManager.instance.pool.GetWep(prefabId); // 미리 생성한 무기 오브젝트를 가져옴
  
            float angle = angleStep * index;
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.right; // 정해진 각도에 해당하는 방향 계산

            crossfire.GetComponent<Bullet>().Init(damage, per, speed*GameManager.instance.player.bulletSpeed, direction); // 방향 벡터는 사용하지 않으므로 Vector3.zero로 설정
            crossfire.transform.position = transform.position + direction * 1.5f; // 플레이어 위치와 방향을 곱하여 무기 오브젝트의 위치 설정
            crossfire.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            crossfire.transform.parent = transform;
            crossfire.transform.localScale = new Vector3(size* player.WeaponSize, size * player.WeaponSize, size * player.WeaponSize);


        }
    }

    void Laserbow()
    {

        float angleStep = 360f / count;  // 발사 각도 간격 계산

        for (int i = 0; i < count; i++)
        {
            Transform fire = GameManager.instance.pool.GetWep(prefabId).transform;
            GameObject laservfx = GameManager.instance.pool.GetVfx(1);
            fire.transform.parent = transform;
           


            fire.position = transform.GetChild(i).GetChild(0).transform.position;
            laservfx.transform.position = transform.GetChild(i).GetChild(0).transform.position;
            laservfx.transform.parent = transform.GetChild(i).GetChild(0);




            float angle = i * angleStep;  // 발사할 각도 계산

            Vector2 dir = Quaternion.Euler(0f, 0f, angle -45f) * Vector2.right;  // 45도를 추가하여 각도를 기울임
            fire.rotation = Quaternion.FromToRotation(Vector3.right, dir);
            fire.transform.localScale = new Vector3(size * player.WeaponSize, size * player.WeaponSize, size * player.WeaponSize);
            fire.GetComponent<Bullet>().Init(damage, per, speed, dir);
            StartCoroutine(ObjFalse(laservfx, 0.6f));
        }
    }

    IEnumerator Energyrain()
    {
        /// 떨어지는 지점에서 좌상단 생성
        /// 그 방향으로 일정한 속력을 가지고 출발
        /// 도착하면? false.... circle 생성 
        float range = 6f;
        float distance = 4f; // 대각선으로 이동할 거리

        Vector2 playerPosition = player.transform.position; 

        float randomX = Random.Range(playerPosition.x - range, playerPosition.x + range);   
        float randomY = Random.Range(playerPosition.y - range, playerPosition.y + range);

        Vector2 targetPos = new Vector3(randomX, randomY);

        Vector2 startPos = targetPos + new Vector2(-distance, distance);
        Vector2 dir = Quaternion.Euler(0f, 0f, 360 -45f) * Vector2.right;  // 45도를 추가하여 각도를 기울임

        GameObject rain = GameManager.instance.pool.GetWep(prefabId);


        rain.transform.parent = GameManager.instance.pool.transform.GetChild(2);
        rain.transform.position = startPos;
        rain.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        rain.transform.localScale = new Vector3(size * player.WeaponSize, size * player.WeaponSize, size * player.WeaponSize);
        rain.GetComponent<Bullet>().Init(0, per, 7.5f, dir);

        yield return new WaitForSeconds(0.7f);

        rain.gameObject.SetActive(false);

        GameObject circle = GameManager.instance.pool.GetVfx(3);
        circle.transform.parent = GameManager.instance.pool.transform.GetChild(2);
        circle.transform.position = targetPos;
        circle.transform.localScale = new Vector3(size*8 * player.WeaponSize, size*8 * player.WeaponSize, size *8* player.WeaponSize);

        circle.GetComponent<Bullet>().Init(damage, -1, speed, dir);
        StartCoroutine(ObjFalse(circle, 3f));


    }


    public void ActWep()
    {
        #region 동작
        switch (id)
        {
            case "Sword":
                transform.Rotate(Vector3.back * speed * player.bulletSpeed * Time.deltaTime);
                break;

            case "Fire":
                if (CheckCooldown())
                {
                    Fire();
                }
                break;

            case "Boom":
                if (CheckCooldown())
                {
                    StartCoroutine(Boom());
                }
                break;

            case "Crossfire":
                if (CheckCooldown())
                {
                    Crossfire();
                }
                break;

            case "Laserbow":
                if (CheckCooldown())
                {
                    Laserbow();
                }
                break;
            case "Energyrain":
                if (CheckCooldown())
                {
                    StartCoroutine(Energyrain());
                }
                break;
        }
        #endregion
    }

   
    public void WeaponCheck()
    {
  
        if (this.id=="Sword")
        {
            switch (this.weaponlevel)
            {
                case 1:     // damage, cool, size, speed, per, count
                    WeaponX(1f, 1f, 1f,  1f, 0f, 0f);
                    break;
                case 2:
                    WeaponX(1.2f, 1f, 1f, 1f, 0f, 0f);
                    break;
                case 3:
                    WeaponX(1.2f, 1f, 1.2f, 1f, 0f, 1f);
                    break;
                case 4:
                    WeaponX(1.4f, 1f, 1.2f, 1f, 0f, 1f);
                    break;
                case 5:
                    WeaponX(1.4f, 1f, 1.4f, 1.25f, 0f, 2f);
                    break;
                case 6:
                    WeaponX(1.5f, 1f, 1.4f, 1.5f, 0f, 3f);
                    break;

            }
        }
        else if (this.id == "Fire")
        {
            switch (this.weaponlevel)
            {
                case 1:     // damage, cool, size, speed, per, count
                    WeaponX(1f, 1f, 1f, 1f, 0f, 0f);
                    break;
                case 2:
                    WeaponX(1.2f, 1f, 1f, 1f, 0f, 0f); // damage 20%
                    break;
                case 3:
                    WeaponX(1.2f, 0.8f, 1f, 1f, 0f, 0f); // cool 20% 
                    break;
                case 4:
                    WeaponX(1.4f, 0.6f, 1f, 1f, 0f, 0f); //damage 20% cool 20%
                    break;
                case 5:
                    WeaponX(1.4f, 0.4f, 1f, 1f, 0f, 0f); // cool 20%
                    break;
                case 6:
                    WeaponX(1.6f, 0.4f, 1f, 1.15f, 0f, 0f); // damage 20% speed 15%
                    break;
            }
        }
        else if (this.id == "Boom")
        {
            switch (this.weaponlevel)
            {
                case 1:     // damage, cool, size, speed, per, count
                    WeaponX(1f, 1f, 1f, 1f, 0f, 0f);
                    break;
                case 2:
                    WeaponX(1.3f, 1f, 1f, 1f, 0f, 0f);  // damage 30%
                    break;
                case 3:
                    WeaponX(1.3f, 1f, 1.3f, 1f, 0f, 0f);    // size 30%
                    break;
                case 4:
                    WeaponX(1.3f, 0.8f, 1.3f, 1f, 0f, 0f);  // cool 20%
                    break;
                case 5:
                    WeaponX(1.3f, 0.7f, 1.4f, 1f, 0f, 0f);  // cool 10%, size 10%
                    break;
                case 6:
                    WeaponX(1.6f, 0.7f, 1.5f, 1f, 0f, 0f); // damage 30%, size 10%
                    break;
            }
        }
        else if (this.id == "Crossfire")
        {
            switch (this.weaponlevel)
            {
                case 1:     // damage, cool, size, speed, per, count
                    WeaponX(1f, 1f, 1f, 1f, 0f, 0f);
                    break;
                case 2:
                    WeaponX(1.2f, 1f, 1f, 1f, 0f, 0f);  // damage 20%
                    break;
                case 3:
                    WeaponX(1.2f, 1f, 1f, 1f, 0f, 4f);  // count 4 
                    break;
                case 4:
                    WeaponX(1.2f, 0.8f, 1f, 1f, 0f, 4f);    // cool 20%
                    break;
                case 5:
                    WeaponX(1.4f, 0.8f, 1.2f, 1f, 0f, 4f); // damage 20% size 20%
                    break;
                case 6:
                    WeaponX(1.6f, 0.8f, 1.2f, 1f, 0f, 8f);  // damage 20% count 4
                    break;
            }
        }
        else if (this.id == "Laserbow")
        {
            switch (this.weaponlevel)
            {
                case 1:     // damage, cool, size, speed, per, count
                    WeaponX(1f, 1f, 1f, 1f, 0f, 0f);
                    break;
                case 2:
                    WeaponX(1.2f, 1f, 1f, 1f, 0f, 0f); // damage 20%
                    break;
                case 3:
                    WeaponX(1.2f, 0.8f, 1f, 1f, 0f, 0f); // cool 20%
                    break;
                case 4:
                    WeaponX(1.2f, 0.8f, 1.2f, 1.2f, 0f, 0f); // size 20% speed 20%
                    break;
                case 5:
                    WeaponX(1.4f, 0.8f, 1.2f, 1.2f, 0f, 0f);  //damage 20%,
                    break;
                case 6:
                    WeaponX(1.6f, 0.8f, 1.35f, 1.2f, 0f, 0f);   //damage 20%, size 15%
                    break;
            }
        }
        else if (this.id == "Energyrain")
        {
            switch (this.weaponlevel)
            {
                case 1:     // damage, cool, size, speed, per, count
                    WeaponX(1f, 1f, 1f, 1f, 0f, 0f);
                    break;
                case 2:
                    WeaponX(1.2f, 1f, 1f, 1f, 0f, 0f); // damage 20%
                    break;
                case 3:
                    WeaponX(1.2f, 0.8f, 1f, 1f, 0f, 0f); // cool 20%
                    break;
                case 4:
                    WeaponX(1.2f, 0.8f, 1.2f, 1.2f, 0f, 0f); // size 20% speed 20%
                    break;
                case 5:
                    WeaponX(1.4f, 0.8f, 1.2f, 1.2f, 0f, 0f);  //damage 20%,
                    break;
                case 6:
                    WeaponX(1.6f, 0.8f, 1.35f, 1.2f, 0f, 0f);   //damage 20%, size 15%
                    break;
            }
        }


    }
    private void WeaponX(float damageX, float coolX, float sizeX, float speedX, float perX, float countX)
    {
        WeaponSet(
            Initdamage * damageX,
            Initcool * coolX,
            Initsize * sizeX,
            Initspeed * speedX,
            Initper + perX,
            Initcount + countX

        );
    }

    public void WeaponSet(float damage,  float cool, float size,  float speed, float per,float count)        // 레벨업마다 데미지와 갯수 늘림
    {
        this.damage = damage;       // 10
        this.cool = cool;
        this.size = size;
        this.speed = speed;
        this.per = per;
        this.count = count;        // +1

        if (id == "Sword")
        {
            Sword();
        }
    }
    

    bool CheckCooldown()
    {
        timer += Time.deltaTime;
        if (timer > cool * player.WeaponCool)
        {
            timer = 0f;
            return true;
        }
        return false;
    }
    IEnumerator ObjFalse(GameObject Obj, float time)
    {
        yield return new WaitForSeconds(time);
        Obj.SetActive(false);
    }
}
