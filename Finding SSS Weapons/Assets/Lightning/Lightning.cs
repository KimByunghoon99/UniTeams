using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float damage;

    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
