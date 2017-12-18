using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Text levelInt;
    public Text levelTime;

    private PlayerTimer player;
    public static bool hasCollectedCollectible = false;
    // Use this for initialization
    void Start () {
        hasCollectedCollectible = false;

        player = GameObject.FindObjectOfType<PlayerTimer>();
        
        levelInt.text = "Level " + SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
        float time = player.timeScore;
        if (hasCollectedCollectible)
        {
            if (time < 5f)
                time /= 2f;
            else time -= 5f;
        }

        levelTime.text = "Time: " + Math.Round(time, 2);
	}
}
