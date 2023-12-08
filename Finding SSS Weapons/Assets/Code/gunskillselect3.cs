using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gunskillselect3 : MonoBehaviour
{
    public void SkillSelectButtonClicked()
    {
        PlayerPrefs.SetFloat("GunIndex", 3);
        ChangeToGameScene();
    }

    void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScens");
    }
}
