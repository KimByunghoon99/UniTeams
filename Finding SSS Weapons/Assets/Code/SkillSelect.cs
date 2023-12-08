using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSelect : MonoBehaviour
{
    public void SkillSelectButtonClicked()
    {
        PlayerPrefs.SetFloat("SelectedIndex", 1);
        ChangeToGameScene();
    }

    void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScens");
    }
}
