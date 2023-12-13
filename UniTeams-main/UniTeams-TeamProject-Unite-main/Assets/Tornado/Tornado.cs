using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    // 토네이도의 데미지, 지속시간, 공격 간격, 따라가는 속도 변수
    public float damage = 10f;
    public float duration = 20f;
    public float attackInterval = 1f;
    public float followSpeed = 5f;
    public float followRange = 10f;

    private float elapsedTime = 0f;
    private Animator animator;
    private Transform targetEnemy;

    private void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        // 시작 시 TornadoStart 애니메이션 실행
        PlayAnimation("TornadoStart");
        animator.SetBool("Enter", true);
        animator.SetBool("End", false);

        // 초기에 가장 가까운 적 찾기
        FindClosestEnemy();
    }

    private void Update()
    {
        // 경과 시간 측정
        elapsedTime += Time.deltaTime;

        // 토네이도 지속 중일 때
        if (elapsedTime < duration)
        {
            // 가장 가까운 적 따라가기 및 공격 체크
            FollowClosestEnemy();
        }
        else
        {
            // 지속 시간이 끝나면 TornadoEnd 애니메이션 실행 후 오브젝트 파괴
            animator.SetBool("End", true);
            animator.SetBool("Enter", false);
            Destroy(gameObject, 1f); // "TornadoEnd" 애니메이션 재생 후 1초 뒤에 오브젝트 파괴
        }
    }

    // 가장 가까운 적 따라가기
    private void FollowClosestEnemy()
    {
        // 현재 타겟이 null이거나 타겟이 파괴되었을 경우 새로운 타겟 찾기
        if (targetEnemy == null || !targetEnemy.gameObject.activeSelf)
        {
            FindClosestEnemy();
        }

        // 타겟이 있을 때 따라가기
        if (targetEnemy != null)
        {
            Vector3 direction = targetEnemy.position - transform.position;
            transform.Translate(direction.normalized * followSpeed * Time.deltaTime);
        }
    }

    private void FindClosestEnemy()
    {
        // 특정 반경 내에서 Enemy 태그를 가진 적 찾기
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, followRange);

        if (colliders.Length > 0)
        {
            // 가장 가까운 적 찾기
            float closestDistance = float.MaxValue;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetEnemy = collider.transform;
                    }
                }
            }
            Debug.Log(targetEnemy);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.layer = LayerMask.NameToLayer("NoCollision");
            Invoke("ResetLayer", attackInterval);
        }
    }


    void ResetLayer()
    {
        // 충돌 가능해지면 다시 원래의 Layer로 변경
        gameObject.layer = LayerMask.NameToLayer("Default");
    }


    // 애니메이션 재생 함수
     void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}