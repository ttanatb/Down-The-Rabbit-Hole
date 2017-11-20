using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //stores the dimensions of the deadzone for the camera
    public float deadX;
    public float deadY;  
    //player game object for the camera to follow
    public GameObject player;
    //position storage vectors
    Vector3 playerPosition;
    Vector3 cameraPosition;

    //damping velocity
    //Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        playerPosition = player.transform.position;
        //setting the camera to the player's position to start
        cameraPosition = playerPosition;
        cameraPosition.z -= 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //getting the player's position each frame to check
        playerPosition = player.transform.position;
        //if The player is outside the deadzone move the deadzone to take over the player
        if (playerPosition.x > cameraPosition.x + deadX)
        {
            cameraPosition.x = playerPosition.x - deadX;
        }
        else if (playerPosition.x < cameraPosition.x - deadX)
        {
            cameraPosition.x = playerPosition.x + deadX;
        }

        //same for y values
        if (playerPosition.y > cameraPosition.y + deadY)
        {
            cameraPosition.y = playerPosition.y - deadY;
        }
        else if (playerPosition.y < cameraPosition.y - deadY)
        {
            cameraPosition.y = playerPosition.y + deadY;
        }

        //changing the position of the camera
        //gameObject.transform.position= Vector3.SmoothDamp(gameObject.transform.position,cameraPosition, ref velocity, 0.25f);
        gameObject.transform.position = cameraPosition;
    }


    
}