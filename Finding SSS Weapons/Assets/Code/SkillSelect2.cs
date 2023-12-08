using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSelect2 : MonoBehaviour
{

    public void SkillSelectButtonClicked()
    {
        PlayerPrefs.SetFloat("SelectedIndex", 2);
        PlayerPrefs.Save();
        ChangeToGameScene();
    }

    void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScens");
    }
}
