using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public GameObject textArrayObject;
    public string[] binarySearchArray;
    public string[] BFSArray;
    public string[] selectionSortArray;

    public void BinarySearch()
    {
        PlayerPrefArray.SetStringArray("binarySearch", binarySearchArray);
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BFS()
    {
        PlayerPrefArray.SetStringArray("bfs", BFSArray);
        PlayerPrefs.SetInt("level", 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SelectionSort()
    {
        PlayerPrefArray.SetStringArray("selectionSort", selectionSortArray);
        PlayerPrefs.SetInt("level", 3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        Debug.Log("Quit button pressed.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
