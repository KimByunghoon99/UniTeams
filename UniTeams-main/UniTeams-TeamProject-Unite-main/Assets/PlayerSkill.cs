using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    //우클릭 이동 좌클릭 평타가 사용감이 안좋을 것 같음
    //대안 1: 이동을 wasd, 1234로 스킬 장전, 좌클릭 평타 또는 스킬 시전 => 이동 스크립트도 바꿔야 하고 스킬 키할당도 바꿔야 함
    //대안 2: 이동을 우클릭, qwer로 스킬 장전, 스페이스바 평타 또는 스킬 시전 => 여기 스크립트에서 Input.GetMouseButtonDown(0)을 Input.GetKeyDown(KeyCode.Space)로만 바꾸면 됨 , 스페이스 너무 시끄럽다 싶으면 다른키도 가능

    //11번 12번 스킬 미완성..
    //방법 1: 그냥 날먹 스킬 넣기 ex) 타겟팅으로 저주 걸기, 회복 스킬
    //방법 2: 10번 회오리 스킬을 분화 시켜서 11번은 작은회오리 여러개 12번은 추적 안하는 대신 제일 크고 센 회오리 설치기
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
                        Debug.Log("스킬 범위 안에 적이 존재하지 않습니다.");
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
        // LightningGenerator 스크립트의 GenerateLightning 메서드를 호출하여 번개를 생성
        StartCoroutine(lightningGenerator.GenerateLightning());
    }
    bool IsEnemyInRange()
    {
        CalTargetPos();
        followRange = TornadoPrefab.GetComponent<Tornado>().followRange;
        // targetPos 주변에 반경 followRange 안에 있는 모든 콜라이더들을 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, followRange);

        // 가져온 콜라이더들을 순회하면서 Enemy 태그를 가진 물체가 있는지 확인
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                // Enemy 태그를 가진 물체가 있으면 true 반환
                return true;
            }
        }

        // Enemy 태그를 가진 물체가 없으면 false 반환
        return false;
    }
}
