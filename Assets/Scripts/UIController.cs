using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Text levelInt;
    public Text levelTime;

    private PlayerTimer player;
	// Use this for initialization
	void Start () {

  
        player = GameObject.FindObjectOfType<PlayerTimer>();
        
        levelInt.text = "Level " + SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
        levelTime.text = "Time: " + player.timeScore;
	}
}
