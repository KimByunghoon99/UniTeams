using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGenerator : MonoBehaviour
{
    public GameObject missilePrefab;
    float span = 1.0f;
    float delta = 0;


    public void generateMissile(Vector3 targetPos)
    {
        GameObject go = Instantiate(missilePrefab);
        go.transform.position = new Vector3(targetPos.x + 2.8f, targetPos.y + 4, 0);
        go.GetComponent<Missile>().RotateMove(targetPos);
    }

}
