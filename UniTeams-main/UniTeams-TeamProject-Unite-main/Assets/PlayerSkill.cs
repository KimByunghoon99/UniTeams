using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject RangeIndicator;
    public GameObject BulletGenerator, MissileGenerator, KnifeGenerator, TornadoGenerator;
    public LightningGenerator lightningGenerator;
    public GameObject ShieldPrefab;
    public GameObject HealPrefab;
    public GameObject TornadoPrefab;
    public GameObject TargetLightningPrefab;
    Vector3 mousePos, transPos, targetPos;
    Vector2 direction;
    public int attackRange = 10;
    public float ShieldSpan = 5f;
    public float HealAmount = 100f;
    public float PlayerMaxHP = 300f;
    public int QskillReady = 0;
    public int WskillReady = 0;
    public int EskillReady = 0;
    public int RskillReady = 0;
    public float QCoolTime, WCoolTime, ECoolTime, RCoolTime;
    int skillReady = 0;
    int BaseAttack = 0;
    int ShotStack = 0;
    float followRange = 0f;
    float CoolTime = 0f;
    float QReadyTime, WReadyTime, EReadyTime, RReadyTime = 0f;

    private Transform targetEnemy;


    void Start()
    {
    QCoolTime = 15f;
    WCoolTime = 20f;
    ECoolTime = 10f;
    RCoolTime = 30f;
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
    void CalEnemyDirection()
    {
        if(targetEnemy != null) 
            direction = targetEnemy.position - transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(QskillReady == 0)
            {

            }
            else
            {
                if (Time.time > QReadyTime)
                {
                    BaseAttack = QskillReady;
                    RangeIndicator.GetComponent<RangeIndicator>().setRangeType(BaseAttack);
                    QReadyTime = Time.time + QCoolTime;
                }
                else
                {
                    if (BaseAttack == 0) //��Ÿ��ȭ���ӽð��� ��Ÿ�� ���� �ð�
                    {
                        CoolTime = QReadyTime - Time.time;
                        RangeIndicator.GetComponent<RangeIndicator>().BaseAttackShowCoolTime(CoolTime);
                    }
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (WskillReady == 0)
            {

            }
            else
            {
                if (Time.time > WReadyTime)
                {
                    skillReady = WskillReady;
                    RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);

                }
                else
                {
                    CoolTime = WReadyTime - Time.time;
                    RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EskillReady == 0)
            {

            }
            else
            {
                if (Time.time > EReadyTime)
                {
                    skillReady = EskillReady;
                    RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);

                }
                else
                {
                    CoolTime = EReadyTime - Time.time;
                    RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (RskillReady == 0)
            {

            }
            else
            {
                if (Time.time > RReadyTime)
                {
                    skillReady = RskillReady;
                    RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);
                }
                else
                {
                    CoolTime = RReadyTime - Time.time;
                    RangeIndicator.GetComponent<RangeIndicator>().ShowCoolTime(CoolTime);
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && skillReady != 0)
        {
            switch (skillReady)
            {
                case 4:
                    CalTargetPos();
                    MissileGenerator.GetComponent<MissileGenerator>().generateMissile(targetPos);
                    WReadyTime = Time.time + WCoolTime;
                    break;

                case 5:
                    ActivateLightningSkill();
                    WReadyTime = Time.time + WCoolTime;
                    break;

                case 6:
                    GameObject Shield = Instantiate(ShieldPrefab);
                    gameObject.layer = LayerMask.NameToLayer("NoCollision");
                    Invoke("ResetLayer", ShieldSpan);
                    Destroy(Shield, ShieldSpan);
                    WReadyTime = Time.time + WCoolTime;
                    break;

                case 7:
                case 8:
                case 9:
                    CalDirection();
                    KnifeGenerator.GetComponent<KnifeGenerator>().GenerateKnife(skillReady, direction);
                    EReadyTime = Time.time + ECoolTime;
                    break;
                case 10:
                    CalTargetPos();
                    if (IsEnemyInRange())
                    {
                        TornadoGenerator.GetComponent<TornadoGenerator>().GenerateTornado(targetPos);
                        RReadyTime = Time.time + RCoolTime;
                    }
                    else
                    {
                        Debug.Log("��ų ���� �ȿ� ���� �������� �ʽ��ϴ�.");
                    }
                    break;
                case 11:
                    PlayerMoveToClick player = GetComponent<PlayerMoveToClick>();
                    if (player.playerHP > PlayerMaxHP - HealAmount) //Ǯ�Ǳ����� ü�� ä����
                        player.playerHP = PlayerMaxHP;
                    else if (player.playerHP == PlayerMaxHP) { // Ǯ�Ǹ� �ߵ� �ȵ�
                        Debug.Log("�÷��̾��� ü���� �̹� ����á���ϴ�.");
                        break;
                    }   
                    else
                        player.playerHP += HealAmount;
                    GameObject Heal = Instantiate(HealPrefab);
                    Destroy(Heal,1.1f);                   
                    Heal.transform.position = transform.position;
                    RReadyTime = Time.time + RCoolTime;
                    break;
                case 12:
                    Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
                    if (hit.collider != null)
                    {
                        if(hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Monster"))
                        {
                            targetPos = hit.transform.position;
                            Instantiate(TargetLightningPrefab, targetPos, Quaternion.identity);
                            RReadyTime = Time.time + RCoolTime;
                        }
                        else
                        {
                            Debug.Log("���� �������ּ���.");
                        }
                    }
                    else
                    {
                        Debug.Log("�ƹ��͵� ���õ��� �ʾҽ��ϴ�.");
                    }
                    break;
                default:
                    break;            
            }
            skillReady = 0;

        }
        FindClosestEnemy();
        CalEnemyDirection();
        ShotStack = BulletGenerator.GetComponent<BulletGenerator>().GenerateBullet(BaseAttack, direction, transform.position);
        if (BaseAttack == 1 && ShotStack > 20) 
        {
            BaseAttack = 0;
            RangeIndicator.GetComponent<RangeIndicator>().HideBaseAttackIcon();
        }
        if (BaseAttack == 2 && ShotStack > 10)
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
    void ActivateLightningSkill()
    {
        if (lightningGenerator == null)
        {
            Debug.LogError("lightningGenerator�� null�Դϴ�. ActivateLightningSkill�� ȣ���ϱ� ���� �Ҵ�Ǿ����� Ȯ���Ͻʽÿ�.");
        }
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
            if (collider.CompareTag("Enemy") || collider.CompareTag("Monster"))
            {
                // Enemy �±׸� ���� ��ü�� ������ true ��ȯ
                return true;
            }
        }

        // Enemy �±׸� ���� ��ü�� ������ false ��ȯ
        return false;
    }

    private void FindClosestEnemy()
    {
        // Ư�� �ݰ� ������ Enemy �±׸� ���� �� ã��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        if (colliders.Length > 0)
        {
            // ���� ����� �� ã��
            float closestDistance = float.MaxValue;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy") || collider.CompareTag("Monster"))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetEnemy = collider.transform;
                    }
                }
            }
        }
    }
    void ResetLayer()
    {
        // �浹 ���������� �ٽ� ������ Layer�� ����
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
