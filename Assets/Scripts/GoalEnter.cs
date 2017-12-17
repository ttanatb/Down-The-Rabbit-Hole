using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEnter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Checks to see if the player enters
    /// If they do the player wins
    /// </summary>
    /// <param name="other">Collider for the gameobject entering the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("You win, you dork!");
        }
    }
}
