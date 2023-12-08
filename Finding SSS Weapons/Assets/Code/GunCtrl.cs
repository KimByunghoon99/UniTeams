using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bullet2;
    public Transform sPoint;
    public float timeBetweenShots;
    public float timeBetweenShots1 = 0.1f; //스킬 1번 재발사 속도
    public int numberOfShots1 = 3; //스킬 1번 단검 날라가는 갯수
    public float fanSpreadAngle = 45f; // 부채꼴의 각도
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

    IEnumerator ShootGun(float shootingAngle) //스킬 1번 기본공격이 3개씩 발사
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

    IEnumerator ShootGun2(float shootingAngle) //스킬 2번 거대한 대포알 1개 발사
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)sPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Instantiate(bullet2, sPoint.position, bulletRotation);
        yield return null; // 한 발만 발사하므로 대기할 필요가 없습니다.
    }

    void ShootGun3() //스킬 3번 기본공격을 3갈래 방향으로 발사
    {
        Vector2 gunDirection = transform.up; // 총이 바라보는 방향
        float startAngle = fanSpreadAngle * (numberOfShots1 - 1) / 2; // 부채꼴 시작 각도 설정

        for (int i = 0; i < numberOfShots1; i++)
        {
            float bulletAngle = startAngle - (i * fanSpreadAngle); // 각 총알의 발사 각도 설정
            Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * -gunDirection; // 총알의 방향 설정 (총의 반대 방향)
            float bulletRotation = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            Quaternion bulletRotationQuat = Quaternion.AngleAxis(bulletRotation, Vector3.forward);
            Instantiate(bullet, sPoint.position, bulletRotationQuat); // 총알 발사
        }
    }
}
