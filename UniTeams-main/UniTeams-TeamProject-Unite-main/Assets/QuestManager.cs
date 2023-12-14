using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int FirstMonsterKill = 0;
    public int MiddleBossMonsterKill = 0;
    public int SecondMonsterKill = 0;
    public GameObject Q1txt;
    public GameObject Q2txt;
    public GameObject Q3txt;
    public GameObject Q4txt;

    // Update is called once per frame
    void Update()
    {
        if (FirstMonsterKill >= 10)
        {
            FirstMonsterKill = -1;
            Debug.Log("10마리 처치 완료");
            Q1txt.SetActive(false);
            Q2txt.SetActive(true);
        }
        if (MiddleBossMonsterKill == 1)
        {
            MiddleBossMonsterKill = -1;
            Debug.Log("중간보스 처치 완료");
            Q2txt.SetActive(false);
            Q3txt.SetActive(true);
        }
        if (SecondMonsterKill >= 15)
        {
            SecondMonsterKill = -1;
            Debug.Log("15마리 처치 완료");
            Q3txt.SetActive(false);
            Q4txt.SetActive(true);
        }
    }
}
