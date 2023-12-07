using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public int per;

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;

    }
    void Start()
    {
        Destroy(gameObject,0.62f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
