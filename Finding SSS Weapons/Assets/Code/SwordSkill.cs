using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill1 : MonoBehaviour
{
    public GameObject Sword;
    public Transform sPoint;
    public float timeBetweenShots = 0.1f; //��ų 1�� ��߻� �ӵ�
    public float timeBetweenShots3 = 1f; //��ų 3�� ��߻� �ӵ�
    public int numberOfShots = 7; //��ų 1�� �ܰ� ���󰡴� ����
    public int numberOfShots1 = 10; // ��ų 2�� ��ä�� ������� ������ �Ѿ� ����
    public float fanSpreadAngle = 15f; // ��ä���� ����
    float skillReady = 0;

    // Update is called once per frame
    void Update()
    {
        float selectedIndex = PlayerPrefs.GetFloat("SelectedIndex", 0);
        PlayerPrefs.Save();

        //ī�޶� ��ũ���� ���콺 �Ÿ��� �Ѱ��� ���� ����
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //���콺 �Ÿ��� ���� ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //�����κ��� ����� ������ ȸ����
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if(Input.GetKeyDown(KeyCode.Q))
        {
            skillReady = 1;
        }

        if (selectedIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (skillReady == 1)
                {
                    StartCoroutine(ShootSword(angle));
                    skillReady = 0;
                }
            }
        }

        if (selectedIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (skillReady == 1)
                {
                    ShootFanSpread();
                    skillReady = 0;
                }
            }
        }

        if (selectedIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (skillReady == 1)
                {
                    StartCoroutine(ShootWithDelay(angle));
                }
            }
        }
    }

    IEnumerator ShootSword(float shootingAngle) //��ų 1�� �����ؼ� �ܰ� 7�� ����
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - (Vector2)sPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Instantiate(Sword, sPoint.position, bulletRotation);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    void ShootFanSpread() //��ų 2�� �ܰ��� 10������ ����
    {
        Vector2 gunDirection = transform.up; // ���� �ٶ󺸴� ����
        float startAngle = fanSpreadAngle * (numberOfShots1 - 1) / 2; // ��ä�� ���� ���� ����

        for (int i = 0; i < numberOfShots1; i++)
        {
            float bulletAngle = startAngle - (i * fanSpreadAngle); // �� �Ѿ��� �߻� ���� ����
            Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * -gunDirection; // �Ѿ��� ���� ���� (���� �ݴ� ����)
            float bulletRotation = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            Quaternion bulletRotationQuat = Quaternion.AngleAxis(bulletRotation, Vector3.forward);
            Instantiate(Sword, sPoint.position, bulletRotationQuat); // �Ѿ� �߻�
        }
    }

    IEnumerator ShootWithDelay(float shootingAngle) //��ų 3�� �ܰ��� 10�ʵ��� 1���� ����
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - (Vector2)sPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Instantiate(Sword, sPoint.position, bulletRotation); // �Ѿ� �߻�
            yield return new WaitForSeconds(timeBetweenShots3);
        }
    }
}
