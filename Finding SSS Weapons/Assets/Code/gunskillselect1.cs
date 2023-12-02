using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gunskillselect1 : MonoBehaviour
{
    public void SkillSelectButtonClicked()
    {
        PlayerPrefs.SetFloat("GunIndex", 1);
        ChangeToGameScene();
    }

    void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScens");
    }
}
