using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSelect3 : MonoBehaviour
{

    public void SkillSelectButtonClicked()
    {
        PlayerPrefs.SetFloat("SelectedIndex", 3);
        PlayerPrefs.Save();
        ChangeToGameScene();
    }

    void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScens");
    }
}
