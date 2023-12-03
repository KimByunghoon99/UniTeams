using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveToClick : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector3 mousePos, transPos, targetPos, dist;
    SpriteRenderer spriter;
    Animator animator;

    void CalTargetPos() // 마우스 클릭 좌표(mousePos)를 바탕으로 목표좌표(targetPos) 구하기, 방향벡터(dist) 구하기
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0); // 목표좌표
        dist = targetPos - transform.position; // 방향벡터
    }

    void Move() // 플레이어 이동 함수
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    public bool Run(Vector3 targetPos) // 현재 달리는중인지 판단
    {
        // 이동하고자하는 좌표 값과 현재 내 위치의 차이를 구한다.
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance >= 0.02f) 
        {
            return true;
        }
        return false; 
    }

    // Start is called before the first frame update
    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 우클릭시
        {
            CalTargetPos(); // 좌표, 방향벡터 계산
            spriter.flipX = dist.x < 0; // 방향벡터 기준으로 좌,우 바라보기
        }
        if(Run(targetPos))
        {
            animator.SetBool("iswalk", true);// walk 애니메이션 처리
            Move();
        }
        else{
            animator.SetBool("iswalk", false);// stand 애니메이션 처리
        }
    }
}
