using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour {

    public Text[] highScoreTextArray;
    HighScoreManager manager;
    public Text gamesPlayed;
    public Text avgAccuracy;
    public Text avgErrors;
    public Text localHighScore;
    float totalGames;
	// Use this for initialization
	void Start()
    {
        totalGames = PlayerPrefs.GetFloat("TotalGames");
        gamesPlayed.text = "Games Played: " + totalGames;
        avgAccuracy.text = "Average Accuracy: " + PlayerPrefs.GetFloat("TotalAccuracy") / totalGames + "%";
        avgErrors.text = "Average Errors: " + PlayerPrefs.GetInt("TotalErrors") / totalGames;
        localHighScore.text = "Local High Score: " + PlayerPrefs.GetFloat("HighWPM") + " WPM";
        // Buffer text while highscore loads
		for (int i = 0; i < highScoreTextArray.Length; i++)
        {
            highScoreTextArray[i].text = i + 1 + ". Fetching...";
        }

        manager = GetComponent<HighScoreManager>();

        StartCoroutine("RefreshHighscores");
	}

    // Called when we load the highscore screen: iterates through array of text objects and displays 
    // username and score associated with that position on leaderboard
    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highScoreTextArray.Length; i++)
        {
            highScoreTextArray[i].text = i + 1 + ". ";
            if(highscoreList.Length > i)
            {
                highScoreTextArray[i].text += highscoreList[i].username + " - " + highscoreList[i].score + " WPM";
            }
        }
    }

    // After 30 seconds pull from the database again to get the most recent leaderboard
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            manager.DownloadNewHighscores();
            yield return new WaitForSeconds(30);
        }
    }

}
