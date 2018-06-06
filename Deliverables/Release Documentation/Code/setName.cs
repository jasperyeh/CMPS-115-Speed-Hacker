using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setName : MonoBehaviour {

    public Text username;

	// Use this for initialization
	void Start()
    {
        username.text = PlayerPrefs.GetString("Name");
	}
	
	// Update is called once per frame
	void Update()
    {
        print(PlayerPrefs.GetString("Name"));
	}
}
