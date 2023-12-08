using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float ShieldSpan;
    public GameObject Player;

    void Start()
    {
        Destroy(gameObject, ShieldSpan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
}
