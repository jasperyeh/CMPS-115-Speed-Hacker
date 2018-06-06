using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public int timeLeft;
    public Text countdownText;

    // Use this for initialization
    void Start()
    {
        timeLeft = 3;
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("" + timeLeft);

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = "CODE!";
            StartCoroutine("WaitOneGodDamnSecond");
        }
    }

    IEnumerator LoseTime()
    {
        print("in coroutine");
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    IEnumerator WaitOneGodDamnSecond()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainGameScene");
    }
}