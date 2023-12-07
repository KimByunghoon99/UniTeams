using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public GameObject Player;
    public GameObject crosshairPrefab; // �̻��� ���ع��� �̹��� ������
    private GameObject crosshairInstance; // ������ �̻��� ���ع��� �̹���
    public GameObject circlePrefab;
    private GameObject circleInstance;

    void Update()
    {
        UpdateRangePosition();

        if (Input.GetMouseButtonDown(0)) { 
            HideRangeIndicator();
        }
    }
    public void setRangeType(int skillReady)
    {
        if (skillReady == 1) // �̻���
        {
            ShowMissileRange();
        }
        if (skillReady == 2) //����
        {
            ShowLightningRange();
        }
    }

    void UpdateRangePosition()
    {
        if (crosshairInstance != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            crosshairInstance.transform.position = mousePosition;
        }
        if(circleInstance != null)
        {
            circleInstance.transform.position = Player.transform.position;
        }
    }

    void ShowMissileRange()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (crosshairInstance == null)
        {
            crosshairInstance = Instantiate(crosshairPrefab, mousePosition, Quaternion.identity);
        }
    }
    void ShowLightningRange()
    {
        if(circleInstance == null)
        {
            circleInstance = Instantiate(circlePrefab, Player.transform.position, Quaternion.identity);
        }
    }

    void HideRangeIndicator()
    {
        if (crosshairInstance != null)
        {
            Destroy(crosshairInstance);
        }
        if (circleInstance != null)
        {
            Destroy(circleInstance);
        }
    }

}
