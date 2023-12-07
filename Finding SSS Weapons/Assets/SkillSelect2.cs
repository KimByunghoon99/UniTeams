using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect2 : MonoBehaviour
{
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;
    public float delayToShowButtons = 5.0f;
    private bool buttonsVisible = false;
    private float timer = 0.0f;

    void Start()
    {
        // 게임 시작 시 버튼 숨기기
        HideButtons();
    }

    void Update()
    {
        // 일정 시간이 경과하면 버튼을 보이게 함
        if (!buttonsVisible)
        {
            timer += Time.deltaTime;

            if (timer >= delayToShowButtons)
            {
                ShowButtons();
                buttonsVisible = true;
            }
        }
    }

    // 버튼 숨기기
    void HideButtons()
    {
        skillButton1.gameObject.SetActive(false);
        skillButton2.gameObject.SetActive(false);
        skillButton3.gameObject.SetActive(false);
    }

    // 버튼 보이기
    void ShowButtons()
    {
        skillButton1.gameObject.SetActive(true);
        skillButton2.gameObject.SetActive(true);
        skillButton3.gameObject.SetActive(true);
    }

    // 버튼 클릭 시 호출되는 함수
    public void OnSkillButtonClick(int buttonIndex)
    {
        HideButtons();
    }
}
