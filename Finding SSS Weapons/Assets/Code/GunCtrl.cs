using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bullet2;
    public Transform sPoint;
    public float timeBetweenShots;
    public float timeBetweenShots1 = 0.1f; //��ų 1�� ��߻� �ӵ�
    public int numberOfShots1 = 3; //��ų 1�� �ܰ� ���󰡴� ����
    public float fanSpreadAngle = 45f; // ��ä���� ����
    public float skillReady = 0;
    public float gunShot = 0;
    public float act = 0;

    private float shotTime;

    void Update()
    {
        float GunsIndex = PlayerPrefs.GetFloat("GunIndex", 0);
        PlayerPrefs.Save();

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= shotTime && gunShot == 0)
            {
                Instantiate(bullet, sPoint.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                shotTime = Time.time + timeBetweenShots;
            }

            if (Time.time >= shotTime && gunShot == 1)
            {
                StartCoroutine(ShootGun(angle));
                shotTime = Time.time + timeBetweenShots;
                act++;

                if (act >= 10)
                {
                    act = 0;
                    gunShot = 0;
                }
            }

            if (Time.time >= shotTime && gunShot == 2)
            {
                ShootGun3();
                shotTime = Time.time + timeBetweenShots;
                act++;

                if (act >= 10)
                {
                    act = 0;
                    gunShot = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillReady = 1;
        }

        if (GunsIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (skillReady == 1)
                {
                    gunShot = 1;
                    skillReady = 0;
                }
            }
        }

        if (GunsIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Time.time >= shotTime && skillReady == 1)
                {
                    StartCoroutine(ShootGun2(angle));
                    shotTime = Time.time + timeBetweenShots;
                    skillReady = 0;
                }
            }
        }

        if (GunsIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (skillReady == 1)
                {
                    gunShot = 2;
                    skillReady = 0;
                }
            }
        }
    }

    IEnumerator ShootGun(float shootingAngle) //��ų 1�� �⺻������ 3���� �߻�
    {
        for (int i = 0; i < numberOfShots1; i++)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - (Vector2)sPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Instantiate(bullet, sPoint.position, bulletRotation);
            yield return new WaitForSeconds(timeBetweenShots1);
        }
    }

    IEnumerator ShootGun2(float shootingAngle) //��ų 2�� �Ŵ��� ������ 1�� �߻�
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)sPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Instantiate(bullet2, sPoint.position, bulletRotation);
        yield return null; // �� �߸� �߻��ϹǷ� ����� �ʿ䰡 �����ϴ�.
    }

    void ShootGun3() //��ų 3�� �⺻������ 3���� �������� �߻�
    {
        Vector2 gunDirection = transform.up; // ���� �ٶ󺸴� ����
        float startAngle = fanSpreadAngle * (numberOfShots1 - 1) / 2; // ��ä�� ���� ���� ����

        for (int i = 0; i < numberOfShots1; i++)
        {
            float bulletAngle = startAngle - (i * fanSpreadAngle); // �� �Ѿ��� �߻� ���� ����
            Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * -gunDirection; // �Ѿ��� ���� ���� (���� �ݴ� ����)
            float bulletRotation = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            Quaternion bulletRotationQuat = Quaternion.AngleAxis(bulletRotation, Vector3.forward);
            Instantiate(bullet, sPoint.position, bulletRotationQuat); // �Ѿ� �߻�
        }
    }
}
