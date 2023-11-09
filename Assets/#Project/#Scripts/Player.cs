using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [Header(" # PlayerMove")]
    [SerializeField]
    public float speed;
    [HideInInspector]
    public Vector3 inputVec;

    public Scanner scanner;
    private PlayerSkill playerskill;

    [Header("# Init")]
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    [Header("# Player Stat")]
    public float baseDamage; // 기본 데미지
    public float critMultiplier; // 치명타 배수
    public float critChance; // 치명타 확률
    public float bulletSpeed;
    public float WeaponSize;
    public float WeaponCount;
    public float WeaponCool;

    [Header("# has Weapon")]
    public List<WeaponStatus> weaponStatusList = new List<WeaponStatus>();


    [System.Serializable]
    public class WeaponStatus
    {
        public string weaponID;
        public bool isActive;
        public int WepLv;

        public WeaponStatus(string weaponID, bool isActive, int WepLv)
        {
            this.weaponID = weaponID;
            this.isActive = isActive;
            this.WepLv = WepLv;
        }

         
    }
    /* public void SomeMethod()
    {
        // weaponStatusList에 접근하여 사용할 수 있음
        foreach (WeaponStatus weaponStatus in weaponStatusList)
        {
            // ...
        }
    }*/

    Dictionary<KeyCode, string> skill_Key = new Dictionary<KeyCode, string>()
    {
        { KeyCode.Space, "Space" },
        { KeyCode.R, "R" },
        { KeyCode.Q, "Q" },
        { KeyCode.W, "W" },
        { KeyCode.E, "E" },
    };

   
    void Awake()
    {
        Init();

        foreach (WeaponStatus weaponStatus in weaponStatusList)
        {
            int index2 = 0; ;
            index2++;
        }
    }


    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal"); // Axis -1 ~ 1 
        inputVec.y = Input.GetAxisRaw("Vertical");   // Raw 1또는 -1만, Raw 없을 시 미끄러지게 구현 가능

        foreach (var kvp in skill_Key)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                string SkillNumber = kvp.Value;
                playerskill.UseSkill(SkillNumber); // 해당하는 스킬 사용
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (WeaponStatus weaponStatus in weaponStatusList)
            {
                // 진화템을 먹었을 때 체크함.
                CheckLevelSix();
                /*
                Debug.Log("Weapon ID: " + weaponStatus.weaponID);
                Debug.Log("Is Active: " + weaponStatus.isActive);
                Debug.Log("Weapon Level: " + weaponStatus.WepLv);*/
            }
        }
       
    }


    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }


    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude); // 벡터의 순수한 크기만 있으면 됨

        if(inputVec.x !=0 )  // == 비교연산자 != 0이 아닐 때, 서로 다른 의미의 연산자
        {
            spriter.flipX = inputVec.x < 0; // inputVec.x 를 측정해 식이 맞으면 true 틀리면 false를 의미
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Skill")
        {
            SkillUp(other.GetComponent<WeaponNumber>().Numbering);
            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "ActiveSkill")
        {
            switch (other.gameObject.name)
            {

                case "Tp":
                    playerskill.AcquireSkill("Tp");
                    break;
                case "Shot":
                    playerskill.AcquireSkill("Shot");
                    break;
                case "Shoot":
                    playerskill.AcquireSkill("Shoot");
                    break;


            }
            Destroy(other.gameObject);

        }



    }

    public float ApplyDamage(float Wepdamage,float armor, float miss)
    {
       float damage = Wepdamage * baseDamage * (1 - armor * 0.01f);
       if (Random.value <critChance) // Random.value는 0에서 1 사이의 랜덤한 값을 반환합니다.
        {
            damage = damage * critMultiplier;
            return damage;
        }
        else       
            return damage;
            
        
        
    }

    public void SkillUp(int WeaponNumbering )
    {
        if (!this.transform.GetChild(0).GetChild(WeaponNumbering).gameObject.activeSelf)
        {
            this.transform.GetChild(0).GetChild(WeaponNumbering).gameObject.SetActive(true);
        }

        this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().weaponlevel++;  // 특정 무기의 레벨업 코드
        this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().WeaponCheck();


        // 스킬을 획득하면 id와 lv을 가져옴
        // 그런 후에 리스트에 이미 그 이름이 존재하면 삭제부터 수행
        // 최신화된 lv을 기준으로 리스트 갱신함.
        string weaponID = this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().id;
        int weaponLevel = this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().weaponlevel;

        weaponStatusList.RemoveAll(weaponStatus => weaponStatus.weaponID == weaponID);
        weaponStatusList.Add(new WeaponStatus(this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().id, true, this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().weaponlevel));

    }
    void CheckLevelSix()
    {
        // Sword와 Fire 오브젝트의 레벨이 6인지 확인
        bool hasSwordLevelSix = weaponStatusList.Exists(weaponStatus => weaponStatus.weaponID == "Sword" && weaponStatus.WepLv == 6);
        bool hasFireLevelSix = weaponStatusList.Exists(weaponStatus => weaponStatus.weaponID == "Fire" && weaponStatus.WepLv == 6);

        // 두 오브젝트의 레벨이 6인 경우에만 특정 메소드 실행
        if (hasSwordLevelSix && hasFireLevelSix)
        {
            // 실행할 메소드 호출, 무기 두개에 다 있어서 다른 스크립트에서 참조 후 실행 필요
            SomeMethod();
        }
    }
    void SomeMethod()
    {
        Debug.Log("무기 강화");
    }
    void Init()
    {
        bulletSpeed = 1f;
        WeaponSize = 1f;
        WeaponCool = 1f;
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        playerskill = GetComponent<PlayerSkill>(); // PlayerSkill 컴포넌트 참조
    }
}
