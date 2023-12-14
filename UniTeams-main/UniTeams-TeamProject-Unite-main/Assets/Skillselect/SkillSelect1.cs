using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect1 : MonoBehaviour
{
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;
    public float delayToShowButtons = 5.0f;
    private bool buttonsVisible = false;
    private float timer = 0.0f;

    void Start()
    {
        // ���� ���� �� ��ư �����
        HideButtons();
    }

    void Update()
    {
        // ���� �ð��� ����ϸ� ��ư�� ���̰� ��
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

    // ��ư �����
    void HideButtons()
    {
        skillButton1.gameObject.SetActive(false);
        skillButton2.gameObject.SetActive(false);
        skillButton3.gameObject.SetActive(false);
    }

    // ��ư ���̱�
    void ShowButtons()
    {
        skillButton1.gameObject.SetActive(true);
        skillButton2.gameObject.SetActive(true);
        skillButton3.gameObject.SetActive(true);
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnSkillButtonClick(int buttonIndex)
    {
            //버튼 인덱스는 각 버튼에 onclick으로 지정 정수를 반환
            GameObject Player = GameObject.FindWithTag("Player");
            Debug.Log("Clicked Button Index: " + buttonIndex);
            switch (buttonIndex)
            {
                case 1:
                case 2:
                case 3:
                    Player.GetComponent<PlayerSkill>().QskillReady = buttonIndex;
                    break;
                case 4:
                case 5:
                case 6:
                    Player.GetComponent<PlayerSkill>().WskillReady = buttonIndex;
                    break;
                case 7:
                case 8:
                case 9:
                    Player.GetComponent<PlayerSkill>().EskillReady = buttonIndex;
                    break;
                case 10:
                case 11:
                case 12:
                    Player.GetComponent<PlayerSkill>().RskillReady = buttonIndex;
                    break;
            }
            HideButtons();
    }
}
