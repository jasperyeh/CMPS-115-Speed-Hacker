using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadStats : MonoBehaviour {

    public Text wpm;
    public Text highestWPM;
    public Text errors;
    public Text accuracy;

    private void Start()
    {
        wpm.text = "WPM: " + PlayerPrefs.GetFloat("WPM").ToString();
        highestWPM.text = "Highest WPM: " + PlayerPrefs.GetFloat("HighWPM").ToString();
        errors.text = "Errors: " + PlayerPrefs.GetInt("Errors").ToString();
        if(PlayerPrefs.GetFloat("Accuracy") <= 0)
        {
            PlayerPrefs.SetFloat("Accuracy", 0);
        }
        accuracy.text = "Accuracy: " + PlayerPrefs.GetFloat("Accuracy").ToString() + "%";
    }

    // Button takes player to main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Button takes player to level select scene
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
