using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    //��Ŭ�� �̵� ��Ŭ�� ��Ÿ�� ��밨�� ������ �� ����
    //��� 1: �̵��� wasd, 1234�� ��ų ����, ��Ŭ�� ��Ÿ �Ǵ� ��ų ���� => �̵� ��ũ��Ʈ�� �ٲ�� �ϰ� ��ų Ű�Ҵ絵 �ٲ�� ��
    //��� 2: �̵��� ��Ŭ��, qwer�� ��ų ����, �����̽��� ��Ÿ �Ǵ� ��ų ���� => ���� ��ũ��Ʈ���� Input.GetMouseButtonDown(0)�� Input.GetKeyDown(KeyCode.Space)�θ� �ٲٸ� �� , �����̽� �ʹ� �ò����� ������ �ٸ�Ű�� ����

    //11�� 12�� ��ų �̿ϼ�..
    //��� 1: �׳� ���� ��ų �ֱ� ex) Ÿ�������� ���� �ɱ�, ȸ�� ��ų
    //��� 2: 10�� ȸ���� ��ų�� ��ȭ ���Ѽ� 11���� ����ȸ���� ������ 12���� ���� ���ϴ� ��� ���� ũ�� �� ȸ���� ��ġ��
    public GameObject SkillSelectManager, RangeIndicator;
    public GameObject BulletGenerator, MissileGenerator, KnifeGenerator, TornadoGenerator;
    public LightningGenerator lightningGenerator;
    public GameObject ShieldPrefab;
    public GameObject TornadoPrefab;
    Vector3 mousePos, transPos, targetPos;
    Vector2 direction;
    public string enemyTag = "Enemy";
    int skillReady = 0;
    int BaseAttack = 0;
    int ShotStack = 0;
    float followRange = 0f;
    float CoolTime = 0f;
    float QcoolTime = 0f;
    float WcoolTime = 0f;
    float EcoolTime = 0f;
    float RcoolTime = 0f;

    void Start()
    {
        
    }

    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }
    void CalDirection()
    {
       direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CoolTime = SkillSelectManager.GetComponent<SkillManage>().QskillCoolTime;
            if (Time.time > QcoolTime)
            {
                RangeIndicator.GetComponent<RangeIndicator>().HideCoolTime();
                BaseAttack = SkillSelectManager.GetComponent<SkillManage>().QskillReady;
                RangeIndicator.GetComponent<RangeIndicator>().setRangeType(BaseAttack);
                QcoolTime = Time.time + CoolTime;
            }
            else
            {               
                RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                skillReady = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Time.time > WcoolTime)
            {
                skillReady = SkillSelectManager.GetComponent<SkillManage>().WskillReady;
                RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);

            }
            else
            {
                RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                skillReady = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time > EcoolTime)
            {
                skillReady = SkillSelectManager.GetComponent<SkillManage>().EskillReady;
                RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);
               
            }
            else
            {
                RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                skillReady = -1;
            }
        }
          

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Time.time > RcoolTime)
            {
                skillReady = SkillSelectManager.GetComponent<SkillManage>().RskillReady;
                RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);               
            }
            else
            {
                RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                skillReady = -1;
            }
        }
        if (Input.GetMouseButtonDown(0) && skillReady != 0)
        {
            switch (skillReady)
            {
                case 4:
                    CalTargetPos();
                    MissileGenerator.GetComponent<MissileGenerator>().generateMissile(targetPos);
                    CoolTime = SkillSelectManager.GetComponent<SkillManage>().WskillCoolTime;
                    WcoolTime = Time.time + CoolTime;
                    break;

                case 5:
                    ActivateLightningSkill();
                    CoolTime = SkillSelectManager.GetComponent<SkillManage>().WskillCoolTime;
                    WcoolTime = Time.time + CoolTime;
                    break;

                case 6:
                    GameObject Shield = Instantiate(ShieldPrefab);
                    CoolTime = SkillSelectManager.GetComponent<SkillManage>().WskillCoolTime;
                    WcoolTime = Time.time + CoolTime;
                    break;

                case 7:
                case 8:
                case 9:
                    CalDirection();
                    Debug.Log(skillReady);
                    KnifeGenerator.GetComponent<KnifeGenerator>().GenerateKnife(skillReady,direction);
                    CoolTime = SkillSelectManager.GetComponent<SkillManage>().EskillCoolTime;
                    EcoolTime = Time.time + CoolTime;
                    break;
                case 10:
                    CalTargetPos();
                    if (IsEnemyInRange())
                    {
                        TornadoGenerator.GetComponent<TornadoGenerator>().GenerateTornado(targetPos);
                        CoolTime = SkillSelectManager.GetComponent<SkillManage>().RskillCoolTime;
                        RcoolTime = Time.time + CoolTime;
                    }
                    else
                    {
                        Debug.Log("��ų ���� �ȿ� ���� �������� �ʽ��ϴ�.");
                    }
                    break;

                default:
                    break;
            }

            skillReady = - 1;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            skillReady = 0;
        }
        if (Input.GetMouseButton(0))
        {
            if (skillReady == 0)
            {
                CalDirection();
                ShotStack = BulletGenerator.GetComponent<BulletGenerator>().GenerateBullet(BaseAttack, direction, transform.position);
                if(BaseAttack==1 && ShotStack > 20)
                {
                    BaseAttack = 0;
                    RangeIndicator.GetComponent<RangeIndicator>().HideBaseAttackIcon();
                }
                if(BaseAttack==2 && ShotStack > 10)
                {
                    BaseAttack = 0;
                    RangeIndicator.GetComponent<RangeIndicator>().HideBaseAttackIcon();

                }
                if (BaseAttack == 3 && ShotStack > 5)
                {
                    BaseAttack = 0;
                    RangeIndicator.GetComponent<RangeIndicator>().HideBaseAttackIcon();

                }
            }
        }
    }
    void ActivateLightningSkill()
    {
        // LightningGenerator ��ũ��Ʈ�� GenerateLightning �޼��带 ȣ���Ͽ� ������ ����
        StartCoroutine(lightningGenerator.GenerateLightning());
    }
    bool IsEnemyInRange()
    {
        CalTargetPos();
        followRange = TornadoPrefab.GetComponent<Tornado>().followRange;
        // targetPos �ֺ��� �ݰ� followRange �ȿ� �ִ� ��� �ݶ��̴����� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, followRange);

        // ������ �ݶ��̴����� ��ȸ�ϸ鼭 Enemy �±׸� ���� ��ü�� �ִ��� Ȯ��
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                // Enemy �±׸� ���� ��ü�� ������ true ��ȯ
                return true;
            }
        }

        // Enemy �±׸� ���� ��ü�� ������ false ��ȯ
        return false;
    }
}
