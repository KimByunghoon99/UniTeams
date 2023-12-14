using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLightning : MonoBehaviour
{
    public float damage;

    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

}
