using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {

    // http://dreamlo.com/lb/ksS4_U8s1062HF7FRZYWLQ9lLmcf5YPU6fCVUD5E4Zng
    const string privateCode = "ksS4_U8s1062HF7FRZYWLQ9lLmcf5YPU6fCVUD5E4Zng";
    const string publicCode = "5afcf20d191a850bcc07e0f9";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highScoresList;
    DisplayHighScores display;

    private void Awake()
    {
        display = GetComponent<DisplayHighScores>();
    }

    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }

    public void DownloadNewHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            display.OnHighscoresDownloaded(highScoresList);
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highScoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highScoresList[i] = new Highscore(username, score);
            //print(highScoresList[i].username + ": " + highScoresList[i].score);
        }
    }

    // In game debug button to reset local high score for Infinity bug
    public void ResetLocalHighScore()
    {
        //PlayerPrefs.DeleteKey("HighWPM");
        PlayerPrefs.DeleteAll();
    }

    public void ResetStats()
    {
        PlayerPrefs.SetFloat("TotalGames", 0);
        PlayerPrefs.SetFloat("TotalAccuracy", 0);
        PlayerPrefs.SetInt("TotalErrors", 0);
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}