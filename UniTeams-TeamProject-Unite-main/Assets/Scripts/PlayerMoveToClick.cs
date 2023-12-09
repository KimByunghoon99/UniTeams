using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveToClick : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    Vector3 mousePos,
        transPos,
        targetPos,
        dist;
    SpriteRenderer spriter;
    Animator animator;

    void CalTargetPos() // ���콺 Ŭ�� ��ǥ(mousePos)�� �������� ��ǥ��ǥ(targetPos) ���ϱ�, ���⺤��(dist) ���ϱ�
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0); // ��ǥ��ǥ
        dist = targetPos - transform.position; // ���⺤��
    }

    void Move() // �÷��̾� �̵� �Լ�
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            Time.deltaTime * speed
        );
    }

    public bool Run(Vector3 targetPos) // ���� �޸��������� �Ǵ�
    {
        // �̵��ϰ����ϴ� ��ǥ ���� ���� �� ��ġ�� ���̸� ���Ѵ�.
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance >= 0.00001f)
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
        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ����
        {
            CalTargetPos(); // ��ǥ, ���⺤�� ���
            spriter.flipX = dist.x < 0; // ���⺤�� �������� ��,�� �ٶ󺸱�
        }
        if (Run(targetPos))
        {
            animator.SetBool("iswalk", true); // walk �ִϸ��̼� ó��
            Move();
        }
        else
        {
            animator.SetBool("iswalk", false); // stand �ִϸ��̼� ó��
        }
    }
}
