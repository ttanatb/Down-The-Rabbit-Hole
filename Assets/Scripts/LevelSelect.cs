﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public int levelToLoad;
    public Text text;
    public string name;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetFloat(("Level" + levelToLoad)) == 0)
        {
            text.text = "Level " + (levelToLoad + 1) + " | Not Completed";
        }
        else
        {
            text.text = "Level " + (levelToLoad + 1) + " | Time: " + PlayerPrefs.GetFloat(("Level" + levelToLoad));
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
