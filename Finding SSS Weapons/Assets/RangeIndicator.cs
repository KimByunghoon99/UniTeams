using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public GameObject circlePrefab; // 범위를 표시할 이미지 프리팹
    private GameObject circleInstance; // 생성된 범위 이미지 인스턴스

    void Update()
    {
        UpdateRangePosition();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowMissileRange();
        }

        if (Input.GetMouseButtonDown(0)) { 
            HideRangeIndicator();
        }
    }

    void UpdateRangePosition()
    {
        if (circleInstance != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            circleInstance.transform.position = mousePosition;
        }
    }

    void ShowMissileRange()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (circleInstance == null)
        {
            circleInstance = Instantiate(circlePrefab, mousePosition, Quaternion.identity);
        }
    }

    void HideRangeIndicator()
    {
        if (circleInstance != null)
        {
            Destroy(circleInstance);
        }
    }
}
