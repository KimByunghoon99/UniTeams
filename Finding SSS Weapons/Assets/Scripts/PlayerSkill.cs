using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject MissileGenerator;
    Vector3 mousePos, transPos, targetPos;
    float skillReady = 0;

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
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (skillReady == 1)
            {
                CalTargetPos();
                MissileGenerator.GetComponent<MissileGenerator>().generateMissile(targetPos);
            }                
            
            skillReady = 0;
        }
    }
}
