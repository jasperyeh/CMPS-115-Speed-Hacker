using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class WriteText : MonoBehaviour {

    public GameObject errorMessage;
    public GameObject syntaxError;
    public GameObject highScoreObj;
    public Text uiText;
    public int greenChar;
    public float wpm;
    public float stopwatch;
    public float numOfChars;
    public float constWPM;
    public float gamesPlayed;
    public bool errorsExist;

    int textArrayElement;
    int textLength;
    public int i;
    public int errorCount;
    int totalErrorCount;
    float tempErrorCount;
    float accuracy;
    float totalAccuracy;
    string[] textArray;
    float totalNumOfChars;

    Text text;

    void Start()
    {
        wpm = 0f;
        stopwatch = 0f;
        numOfChars = 0f;
        constWPM = 0f;

        errorMessage.SetActive(false);
        syntaxError.SetActive(false);

        // Populates the game text array based on the level selected
        if (PlayerPrefs.GetInt("level") == 1)
        {
            textArray = PlayerPrefArray.GetStringArray("binarySearch");
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            textArray = PlayerPrefArray.GetStringArray("bfs");
        }
        else if (PlayerPrefs.GetInt("level") == 3)
        {
            textArray = PlayerPrefArray.GetStringArray("selectionSort");
        }
        else
        {
            print("you shouldn't get here... ever");
        }

        textArrayElement = 0;
        text = GetComponent<Text>();
        i = -1;
        text.text = textArray[textArrayElement];
        textLength = GetComponent<TextHighlight>().textComp.text.Length;
    }

    void Update()
    {
        // Start the timer to base our WPM off of
        stopwatch += 1.0f * Time.deltaTime;

        // Take in user keyboard input
        foreach (char c in Input.inputString)
        {
            // If input is backspace
            if (c == '\b')
            {
                if (uiText.text.Length != 0)
                {
                    numOfChars--;
                    constWPM--;
                    i--;
                    uiText.text = uiText.text.Substring(0, uiText.text.Length - 1);
                }
            }
            else
            {
                if(uiText.text.Length <= 50)
                {
                    i++;
                    uiText.text += c;
                    numOfChars++;
                    constWPM++;
                    if (uiText.text[i] == text.text[i])
                    {
                        GetComponent<TextHighlight>().highlight.GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        GetComponent<TextHighlight>().highlight.GetComponent<Image>().color = Color.red;
                        errorCount++;
                    }
                }
                
            }
        }
        
        // Check if there are any errors and let the player know
        foreach (GameObject obj in GetComponent<TextHighlight>().highlightList){
            if (GetComponent<TextHighlight>().highlightList.Any<GameObject>(tempObj => tempObj.GetComponent<Image>().color == Color.red))
            {
                errorsExist = true;
                syntaxError.SetActive(true);
            }
            else
            {
                errorsExist = false;
                errorMessage.SetActive(false);
                syntaxError.SetActive(false);
            }
        }
        // If the last element you type is incorrect
        // Comment out to get bug that doesn't check if the last thing you type is wrong
        
        if (GetComponent<TextHighlight>().highlightList[text.text.Length - 1].GetComponent<Image>().color == Color.red)
        {
            errorsExist = true;
        }
        
        

        // If we reach the end of our current text we have to type
        if (numOfChars == textLength)
        {
            // If this is the last text element in our overall array of text elements
            // This signifies the end of the game
            // Setting global stats
            if(textArrayElement == textArray.Length)
            {
                gamesPlayed = PlayerPrefs.GetFloat("TotalGames");
                gamesPlayed++;
                PlayerPrefs.SetFloat("TotalGames", gamesPlayed);

                float min = stopwatch / 60f;
                wpm = (constWPM / min) / 5f;
                highScoreObj.GetComponent<HighScore>().currWPM = wpm;
                PlayerPrefs.SetFloat("WPM", wpm);

                PlayerPrefs.SetInt("Errors", errorCount);
                totalErrorCount = PlayerPrefs.GetInt("TotalErrors");
                totalErrorCount += errorCount;
                PlayerPrefs.SetInt("TotalErrors", totalErrorCount);

                accuracy = ((totalNumOfChars - errorCount) / totalNumOfChars) * 100;
                PlayerPrefs.SetFloat("Accuracy", accuracy);
                totalAccuracy = PlayerPrefs.GetFloat("TotalAccuracy");
                totalAccuracy += accuracy;
                PlayerPrefs.SetFloat("TotalAccuracy", totalAccuracy);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            // If we don't have any errors but we aren't finished with all the text elements
            // Move to the next line element in the text array
            // Reset the highlighting array and our user input
            if (errorsExist == false)
            {
                i = -1;
                textArrayElement++;
                text.text = textArray[textArrayElement];
                uiText.text = string.Empty;
                totalNumOfChars += numOfChars;
                numOfChars = 0;
                GetComponent<TextHighlight>().charIndex = 0;
                textLength = GetComponent<TextHighlight>().textComp.text.Length;
                for (int j = 0; j < GetComponent<TextHighlight>().highlightList.Count; j++)
                {
                    Destroy(GetComponent<TextHighlight>().highlightList[j].gameObject);
                }
                GetComponent<TextHighlight>().highlightList.Clear();
            }
            else
            {
                errorMessage.SetActive(true);
            }
        }
    }

}
