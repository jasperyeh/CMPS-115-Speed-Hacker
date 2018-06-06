using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameSelect : MonoBehaviour {

    public Text username;
	
	// Update is called once per frame
	void Update()
    {
        // Takes in keyboard input
        foreach (char c in Input.inputString)
        {
            // If keyboard input is backspace
            if (c == '\b')
            {
                if (username.text.Length != 0)
                {
                    username.text = username.text.Substring(0, username.text.Length - 1);
                }
            }
            else
            {
                if (username.text.Length <= 7)
                {
                    username.text += c;
                }
            }
        }
    }

    // Sets value in database for local user name to be used for high score and in-game display
    public void SetName()
    {
        PlayerPrefs.SetString("Name", username.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Returns to the previous scene
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
