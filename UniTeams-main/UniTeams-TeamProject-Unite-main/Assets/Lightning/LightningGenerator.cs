using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGenerator : MonoBehaviour
{
    public GameObject lightningPrefab; // Lightning 프리팹을 할당하세요.
    public float skillRange = 5f; // 스킬의 범위를 설정하세요.
    public int numberOfTargets = 5; // 생성할 Lightning의 개수를 설정하세요.
    public float delayBetweenStrikes = 0.3f; // 각 Lightning 생성 간의 딜레이를 설정하세요.

    void Start()
    {
    }

    public IEnumerator GenerateLightning()
    {
        // 주변 몬스터를 찾아 리스트에 저장
        List<Transform> nearbyMonsters = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillRange);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                nearbyMonsters.Add(collider.transform);
            }
        }
        nearbyMonsters.Sort((a, b) => Vector2.Distance(transform.position, a.position).CompareTo(Vector2.Distance(transform.position, b.position)));

        // 최대 5개의 몬스터에게 Lightning을 생성
        for (int i = 0; i < Mathf.Min(numberOfTargets, nearbyMonsters.Count); i++)
        {
            Transform targetMonster = nearbyMonsters[i];
            if (targetMonster != null)
            {
                // Lightning을 몬스터의 위치에 생성
                Vector3 spawnPosition = new Vector3(targetMonster.position.x, targetMonster.position.y + 0.3f, targetMonster.position.z);
                GameObject lightning = Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);

                // 몬스터 위치에 생성된 Lightning은 이동하지 않음
                yield return new WaitForSeconds(delayBetweenStrikes);
            }
        }
    }
}