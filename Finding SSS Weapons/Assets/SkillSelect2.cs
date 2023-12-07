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
        HideButtons();
    }
}
