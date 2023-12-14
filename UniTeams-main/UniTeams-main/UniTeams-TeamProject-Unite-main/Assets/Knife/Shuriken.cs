using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float damage;
    public float lifetime = 5f;
    public float shurikenSpeed = 5f;
    public float rotationSpeed = 100f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // ǥâ�� �̵�
        transform.Translate(Vector2.up * shurikenSpeed * Time.deltaTime);

        // ǥâ�� ���� ȸ�� (�̵� ������ ���� ȸ��)
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.localRotation *= Quaternion.Euler(0f, 0f, rotationAmount);
    }

    void DestroyShuriken()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DestroyShuriken();
        }
    }
}
