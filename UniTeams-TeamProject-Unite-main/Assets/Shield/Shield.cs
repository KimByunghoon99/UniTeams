using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float ShieldSpan;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, ShieldSpan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
