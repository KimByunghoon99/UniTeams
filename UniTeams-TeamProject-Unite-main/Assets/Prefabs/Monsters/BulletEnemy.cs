using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float bulletSpeed = 4f;
    protected float timetoLive = 2f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("DestroyBullet", timetoLive);
    }

    public void Fire(Vector3 dir)
    {
        rigid.velocity = dir * bulletSpeed;
    }

    private void Update()
    {
        // rigid.velocity *= 0.998f;

        if (rigid.velocity.magnitude < 0.5)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AudioSource>().Play();
            Debug.Log("체력 깎임 처리");
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
