using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    Vector2 playerPosition;
    Vector2 launchDirection;

    void Awake()
    {
        playerPosition = player.transform.position;
    }

    void Update() { }
}
