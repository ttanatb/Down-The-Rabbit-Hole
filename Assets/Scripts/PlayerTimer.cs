using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimer : MonoBehaviour {
    
    //float to hold how much time has passed thusfar
    public float timeScore;

    //true if the player is currently playing, false if it should be stopped
    public bool timePaused;

    //bool to hold if the player is at the beginning or not
    public bool runStarted;

    //the player
    public GameObject player;



	// Use this for initialization
	void Start () {
        runStarted = false;

	}
	
	// Update is called once per frame
	void Update () {

        if(player.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
        {
            //start the run if the player is moving
            runStarted = true;
            
        }


		if(!timePaused && runStarted)
        {
           
            //update time
            timeScore += Time.deltaTime;
        }
	}

    public void End(int incLevel)
    {
        PlayerPrefs.SetFloat(("Level" + incLevel), timeScore);
        //keep the players time score from increasing
        timePaused = true;
    }
}
