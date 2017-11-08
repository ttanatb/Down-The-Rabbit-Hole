using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    //stores the dimensions of the deadzone for the camera
    public float deadX;
    public float deadY;
    //player game object for the camera to follow
    public GameObject player;
    Vector3 playerPosition;
    Vector3 cameraPosition;

	// Use this for initialization
	void Start () {
        playerPosition = player.transform.position;
        cameraPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //if The player is outside the deadzone move the deadzone to take over the player
        if (playerPosition.x > cameraPosition.x + deadX)
        {
            cameraPosition.x = playerPosition.x - deadX;
        }
        else if (playerPosition.x > cameraPosition.x - deadX)
        {
            cameraPosition.x = playerPosition.x + deadX;
        }

        //same for y values
        if (playerPosition.y > cameraPosition.y + deadY)
        {
            cameraPosition.y = playerPosition.y - deadY;
        }
        else if (playerPosition.y > cameraPosition.y - deadY)
        {
            cameraPosition.y = playerPosition.y + deadY;
        }

        //
        
    }
}
