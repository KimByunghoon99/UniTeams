using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class removeTile : MonoBehaviour
{
    public Tilemap tilemapToDisable;
    public float disableTime = 5.0f; // 타일맵이 사라지기까지의 시간(초)

    private void Start()
    {
        Invoke("DisableTilemap", disableTime); // disableTime 이후에 DisableTilemap 함수 호출
    }

    private void Update()
    {
        
    }

    void DisableTilemap()
    {
        if (tilemapToDisable != null)
        {
            Destroy(tilemapToDisable.gameObject);
        }
    }
}
