using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public GameObject highScoreManager;
    public Text playerName;

    public float currWPM;

    // Update is called once per frame
    void Update() 
    {
        // [UNCOMMENT FOR DEBUG]
        // Debug for resetting the Infinity high score bug
        //if (Input.GetKeyDown(KeyCode.End))
        //{
        //    PlayerPrefs.SetFloat("HighWPM", 0f);
        //}

        // If the high score in game is greater than our local high score, update the leaderboard with this new high score
        if (currWPM > PlayerPrefs.GetFloat("HighWPM", 0f))
        {
            highScoreManager.GetComponent<HighScoreManager>().AddNewHighscore(playerName.text, Mathf.RoundToInt(currWPM));
            PlayerPrefs.SetFloat("HighWPM", currWPM);
        }
	}
}
