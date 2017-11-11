using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SightLine : MonoBehaviour {
    Vector2 position;
    Vector2 playerPosition;
    Vector2 offset;
    GameObject Player;
    float offsetAngle;
    float widthAngle;
    float Radius;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        position = gameObject.GetComponentInParent<Transform>().position;
        playerPosition = Player.GetComponent<Transform>().position;
        offset = playerPosition - position;
        if(offset.normalized.sqrMagnitude < Radius*Radius)//Circle Collision Check;
        {
            if (SightLineCheck())
            {
                Debug.Log("HIT");
            }
                
        }

	}

    bool SightLineCheck()
    {
        if(widthAngle >= 360)//If this is a circle, just return true, since this is calld after a circle collision check
        {
            return true;        
        }
        else
        {
            float rad = Mathf.Atan2(offset.y, offset.x);//Get the angle that the player is from the sightline's source.
            float playerAng = rad * 180 / Mathf.PI; // convert to angles
            
           
            float maxAng = offsetAngle + widthAngle;//Far angle           


            if(maxAng > 360)//if we are looping around 0
            {
                maxAng -= 360;//set the Maximum angle to a small number(below 360).
                //Player is greater than the minimum, or less than the maximum;
                if(playerAng < offsetAngle || playerAng > maxAng)
                {//This is OR not AND because of the looping around 0;
                    return true;
                }               
                return false;
                
            }
            else//If we aren't looping around 0
            {//If the player is Greater than the minimum and less than the maximum
                if (playerAng > offsetAngle && playerAng < maxAng)
                {
                    return true;
                }
                return false;
            }

        }
        
        return false;
    }

}
