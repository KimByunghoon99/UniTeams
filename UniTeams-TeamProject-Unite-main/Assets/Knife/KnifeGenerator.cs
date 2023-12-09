using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeGenerator : MonoBehaviour
{
    public GameObject KnifePrefab;
    public GameObject KnifePrefab1;
    public GameObject KnifePrefab2;
    public float timeBetweenShots = 0.1f; //스킬 1번 재발사 속도
    public int numberOfShots = 7; //스킬 1번 단검 날라가는 갯수
    public int numberOfShots1 = 10; // 스킬 2번 부채꼴 모양으로 나가는 총알 개수
    public float fanSpreadAngle = 15f; // 부채꼴의 각도
    public float timeBetweenShots3 = 1f; //스킬 3번 재발사 속도
    public float skillDuration = 5f;
    public float detectionRadius = 5f; // 주변 적을 탐지할 반경
    public LayerMask enemyLayer; //적을 탐지할 레이어
    private Transform playerTransform;

    void Start()
    {
        // 플레이어의 Transform 컴포넌트 가져오기
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GenerateKnife(int skillReady, Vector2 direction)
    {
        Vector3 playerPos = playerTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if(skillReady == 7)
        {
            StartCoroutine(ShootKnife(angle,playerPos, rotation));
        }
        if(skillReady == 8)
        {
            Vector2 gunDirection = transform.up;
            float startAngle = fanSpreadAngle * (numberOfShots1 - 1) / 2;

            for (int i = 0; i < numberOfShots1; i++)
            {
                float bulletAngle = startAngle - (i * fanSpreadAngle); // 각 총알의 발사 각도 설정
                Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * -gunDirection; // 총알의 방향 설정 (총의 반대 방향)
                float bulletRotation = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
                Quaternion bulletRotationQuat = Quaternion.AngleAxis(bulletRotation, Vector3.forward);
                Instantiate(KnifePrefab1, playerPos, bulletRotationQuat); // 총알 발사
            }
        }
        if (skillReady == 9)
        {
            StartCoroutine(AutoShootKnife(playerPos));
        }
    }

    void Update()
    {
        
    }

    IEnumerator ShootKnife(float angle, Vector3 playerPos, Quaternion rotation) //스킬 1번 연속해서 단검 7개 날림
    {
        Quaternion knifeRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        for (int i = 0; i < numberOfShots; i++) { 
                
                Instantiate(KnifePrefab, playerPos, knifeRotation);
                yield return new WaitForSeconds(timeBetweenShots);
            }
    }

    IEnumerator AutoShootKnife(Vector3 playerPos)
    {
        float elapsedTime = 0f;

        while (elapsedTime < skillDuration)
        {
            playerPos = playerTransform.position;

            // 주위의 적을 찾아서 가장 가까운 적을 향해 단검을 생성하는 로직
            Collider2D[] enemies = Physics2D.OverlapCircleAll(playerPos, detectionRadius, enemyLayer); // 적을 탐지하는 코드, detectionRadius와 enemyLayer는 적의 감지 범위와 레이어에 맞게 설정해야 합니다.

            if (enemies.Length > 0)
            {
                Transform closestEnemy = FindClosestEnemy(playerPos, enemies);
                if (closestEnemy != null)
                {
                    Vector2 enemyDirection = (closestEnemy.position - playerPos).normalized;
                    Quaternion enemyRotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
                    Instantiate(KnifePrefab2, playerPos, enemyRotation);
                }
            }

            yield return new WaitForSeconds(timeBetweenShots3);
            elapsedTime += timeBetweenShots3;
        }

        Transform FindClosestEnemy(Vector3 playerPos, Collider2D[] enemies)
        {
            Transform closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (var enemyCollider in enemies)
            {
                float distance = Vector3.Distance(playerPos, enemyCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemyCollider.transform;
                }
            }

            return closestEnemy;
        }
    }


}
