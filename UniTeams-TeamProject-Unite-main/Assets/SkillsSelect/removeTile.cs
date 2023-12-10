using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class removeTile : MonoBehaviour
{
    public Tilemap tilemapToDisable;
    public float disableTime = 5.0f; // Ÿ�ϸ��� ������������ �ð�(��)

    private void Start()
    {
        Invoke("DisableTilemap", disableTime); // disableTime ���Ŀ� DisableTilemap �Լ� ȣ��
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
