using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused;
    public GameObject pauseUI;
    public GameObject variableHolder;
    public Text goText;
    Color tempColor;
    WriteText writeText;
    TextHighlight textHighlight;

    float wpm;
    float constWPM;
    float stopwatch;
    float numOfChars;
    float highScore;
     
    private void Start()
    {
        writeText = variableHolder.GetComponent<WriteText>();
        textHighlight = variableHolder.GetComponent<TextHighlight>();
        goText.color = new Color(0, 255, 0, 1);
        wpm = writeText.wpm;
        constWPM = writeText.constWPM;
        gamePaused = false;
        stopwatch = writeText.stopwatch;
        numOfChars = writeText.numOfChars;
        pauseUI.SetActive(false);
    }

    void Update()
    {
        tempColor = goText.color;
        tempColor.a -= 0.0075f;
        goText.color = tempColor;
        // Pauses when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseUI.SetActive(false);
        writeText.enabled = true;
        textHighlight.enabled = true;
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        pauseUI.SetActive(true);
        writeText.enabled = false;
        textHighlight.enabled = false;
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void MainMenu()
    {
        wpm = 0f;
        stopwatch = 0f;
        numOfChars = 0f;
        constWPM = 0f;
        SceneManager.LoadScene("MainMenu");
    }
}
