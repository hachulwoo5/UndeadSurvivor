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
    public float baseDamage; // �⺻ ������
    public float critMultiplier; // ġ��Ÿ ���
    public float critChance; // ġ��Ÿ Ȯ��
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
        // weaponStatusList�� �����Ͽ� ����� �� ����
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
        inputVec.y = Input.GetAxisRaw("Vertical");   // Raw 1�Ǵ� -1��, Raw ���� �� �̲������� ���� ����

        foreach (var kvp in skill_Key)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                string SkillNumber = kvp.Value;
                playerskill.UseSkill(SkillNumber); // �ش��ϴ� ��ų ���
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (WeaponStatus weaponStatus in weaponStatusList)
            {
                // ��ȭ���� �Ծ��� �� üũ��.
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
        anim.SetFloat("Speed", inputVec.magnitude); // ������ ������ ũ�⸸ ������ ��

        if(inputVec.x !=0 )  // == �񱳿����� != 0�� �ƴ� ��, ���� �ٸ� �ǹ��� ������
        {
            spriter.flipX = inputVec.x < 0; // inputVec.x �� ������ ���� ������ true Ʋ���� false�� �ǹ�
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
       if (Random.value <critChance) // Random.value�� 0���� 1 ������ ������ ���� ��ȯ�մϴ�.
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

        this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().weaponlevel++;  // Ư�� ������ ������ �ڵ�
        this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().WeaponCheck();


        // ��ų�� ȹ���ϸ� id�� lv�� ������
        // �׷� �Ŀ� ����Ʈ�� �̹� �� �̸��� �����ϸ� �������� ����
        // �ֽ�ȭ�� lv�� �������� ����Ʈ ������.
        string weaponID = this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().id;
        int weaponLevel = this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().weaponlevel;

        weaponStatusList.RemoveAll(weaponStatus => weaponStatus.weaponID == weaponID);
        weaponStatusList.Add(new WeaponStatus(this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().id, true, this.transform.GetChild(0).GetChild(WeaponNumbering).GetComponent<Weapon>().weaponlevel));

    }
    void CheckLevelSix()
    {
        // Sword�� Fire ������Ʈ�� ������ 6���� Ȯ��
        bool hasSwordLevelSix = weaponStatusList.Exists(weaponStatus => weaponStatus.weaponID == "Sword" && weaponStatus.WepLv == 6);
        bool hasFireLevelSix = weaponStatusList.Exists(weaponStatus => weaponStatus.weaponID == "Fire" && weaponStatus.WepLv == 6);

        // �� ������Ʈ�� ������ 6�� ��쿡�� Ư�� �޼ҵ� ����
        if (hasSwordLevelSix && hasFireLevelSix)
        {
            // ������ �޼ҵ� ȣ��, ���� �ΰ��� �� �־ �ٸ� ��ũ��Ʈ���� ���� �� ���� �ʿ�
            SomeMethod();
        }
    }
    void SomeMethod()
    {
        Debug.Log("���� ��ȭ");
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
        playerskill = GetComponent<PlayerSkill>(); // PlayerSkill ������Ʈ ����
    }
}
