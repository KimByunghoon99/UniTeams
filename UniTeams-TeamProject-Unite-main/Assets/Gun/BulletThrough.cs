using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThrough : MonoBehaviour
{
    public float damage;
    public float lifetime = 5f;
    public float BulletSpeed = 10f;
    public int MaxHit = 7;
    private int hit = 0;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);

    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hit++;
            if(hit >= MaxHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
