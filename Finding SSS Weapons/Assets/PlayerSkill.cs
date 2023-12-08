using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject MissileGenerator;
    public GameObject RangeIndicator;
    public LightningGenerator lightningGenerator;
    public GameObject ShieldGenerator;
    Vector3 mousePos, transPos, targetPos;
    int skillReady = 0;

    void Start()
    {
        
    }

    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillReady = 1;
            RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            skillReady = 2;
            RangeIndicator.GetComponent<RangeIndicator>().setRangeType(skillReady);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillReady = 3;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (skillReady == 1)
            {
                CalTargetPos();
                MissileGenerator.GetComponent<MissileGenerator>().generateMissile(targetPos);
            }
            if (skillReady == 2)
            {
                ActivateLightningSkill();
            }
            if (skillReady == 3)
            {
                ShieldGenerator.GetComponent<ShieldGenerator>().GenerateShield();
            }

            skillReady = 0;
        }
    }
    void ActivateLightningSkill()
    {
        // LightningGenerator 스크립트의 GenerateLightning 메서드를 호출하여 번개를 생성
        StartCoroutine(lightningGenerator.GenerateLightning());
    }
}
