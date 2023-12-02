using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill1 : MonoBehaviour
{
    public GameObject Sword;
    public Transform sPoint;
    public float timeBetweenShots = 0.1f; //스킬 1번 재발사 속도
    public float timeBetweenShots3 = 1f; //스킬 3번 재발사 속도
    public int numberOfShots = 7; //스킬 1번 단검 날라가는 갯수
    public int numberOfShots1 = 10; // 스킬 2번 부채꼴 모양으로 나가는 총알 개수
    public float fanSpreadAngle = 15f; // 부채꼴의 각도
    float skillReady = 0;

    // Update is called once per frame
    void Update()
    {
        float selectedIndex = PlayerPrefs.GetFloat("SelectedIndex", 0);
        PlayerPrefs.Save();

        //카메라 스크린의 마우스 거리와 총과의 방향 설정
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //마우스 거리로 부터 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //축으로부터 방향과 각도의 회전값
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

    IEnumerator ShootSword(float shootingAngle) //스킬 1번 연속해서 단검 7개 날림
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

    void ShootFanSpread() //스킬 2번 단검을 10갈래로 날림
    {
        Vector2 gunDirection = transform.up; // 총이 바라보는 방향
        float startAngle = fanSpreadAngle * (numberOfShots1 - 1) / 2; // 부채꼴 시작 각도 설정

        for (int i = 0; i < numberOfShots1; i++)
        {
            float bulletAngle = startAngle - (i * fanSpreadAngle); // 각 총알의 발사 각도 설정
            Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * -gunDirection; // 총알의 방향 설정 (총의 반대 방향)
            float bulletRotation = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            Quaternion bulletRotationQuat = Quaternion.AngleAxis(bulletRotation, Vector3.forward);
            Instantiate(Sword, sPoint.position, bulletRotationQuat); // 총알 발사
        }
    }

    IEnumerator ShootWithDelay(float shootingAngle) //스킬 3번 단검을 10초도안 1개씩 날림
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - (Vector2)sPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Instantiate(Sword, sPoint.position, bulletRotation); // 총알 발사
            yield return new WaitForSeconds(timeBetweenShots3);
        }
    }
}
