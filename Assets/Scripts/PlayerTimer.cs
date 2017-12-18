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
        if (PlayerPrefs.GetInt("colLevel" + incLevel)== 1)
        {
            if(timeScore > 10)
            {
                timeScore -= 5;
            }
            else
            {
                timeScore = timeScore / 2;
            }

            UIController.hasCollectedCollectible = false;
        }
        if (PlayerPrefs.GetFloat("Level" + incLevel) > timeScore || PlayerPrefs.GetFloat("Level" + incLevel) <= 0)
        {//if the new score is lower, or the old score is a default value, replace with the new score        
            PlayerPrefs.SetFloat(("Level" + incLevel), timeScore);
        }

        PlayerPrefs.SetInt("colLevel" + incLevel, 0);//reset collectable value
        //keep the players time score from increasing
        timePaused = true;
    }
}
