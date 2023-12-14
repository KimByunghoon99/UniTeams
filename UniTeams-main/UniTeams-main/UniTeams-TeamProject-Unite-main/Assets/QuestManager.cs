using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int FirstMonsterKill = 0;
    public int SecondMonsterKill = 0;

    // Update is called once per frame
    void Update()
    {
        if (FirstMonsterKill >= 10)
        {
            FirstMonsterKill = -1;
            Debug.Log("10마리 처치 완료");
        }
        if (SecondMonsterKill >= 15)
        {
            SecondMonsterKill = -1;
            Debug.Log("15마리 처치 완료");
        }
    }
}
