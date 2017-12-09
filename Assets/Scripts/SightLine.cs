using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SightLine : MonoBehaviour {
    Vector3 position;
    Vector3 playerPosition;
    Vector3 offset;
    Vector3 leftPos, rightPos, centerPos;
    float minAng, maxAng;

    LineRenderer myRenderer;

    SceneChange sceneChange;
    public GameObject Player;
    public float offsetAngle;
    public float widthAngle;
    public float Radius;
    public Material mat;
   
    // Use this for initialization
    void Start () {
		if (!Player)
        {
            Player = FindObjectOfType<PlayerController>().gameObject;
        }
        myRenderer = GetComponent<LineRenderer>();
        sceneChange = GameObject.Find("SceneManager").GetComponent<SceneChange>();
	}
	
	// Update is called once per frame
	void Update () {
        position = gameObject.transform.position;
        playerPosition = Player.transform.position;
       // Vector3 playerToMonster = playerPosition - position;
       // playerPosition = playerPosition - (playerToMonster.normalized * .35f);
        offset = playerPosition - position;
        offsetAngle = gameObject.transform.rotation.eulerAngles.z;
     

        maxAng = 90 + offsetAngle + (widthAngle / 2);//Far angle           
        minAng = 90 + (offsetAngle - (widthAngle / 2));

        float maxAngRad = maxAng * Mathf.Deg2Rad;
        float minAngRad = minAng * Mathf.Deg2Rad;


        leftPos.x = position.x + Radius * (Mathf.Cos(maxAngRad));
        leftPos.y = position.y + Radius * ( Mathf.Sin(maxAngRad));
        leftPos.z = -.1f;

        centerPos.x = position.x + Radius * (Mathf.Cos((90 + offsetAngle) * Mathf.Deg2Rad));
        centerPos.y = position.y + Radius * (Mathf.Sin((90+ offsetAngle) * Mathf.Deg2Rad));
        centerPos.z = 0.0f;

        rightPos.x = position.x + Radius * (Mathf.Cos(minAngRad));
        rightPos.y = position.y + Radius * ( Mathf.Sin(minAngRad));
        rightPos.z=-.1f;
        position.z = -.1f;

        myRenderer.SetPosition(0, position);
        myRenderer.SetPosition(1, leftPos);
        myRenderer.SetPosition(2, centerPos);
        myRenderer.SetPosition(3, rightPos);

        
        if (offset.sqrMagnitude < Radius*Radius)//Circle Collision Check;
        {
            if (SightLineCheck())
            {                
                Debug.Log("HIT");
                sceneChange.ResetLevel();
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


            if (playerAng<0)
            {
                playerAng += 360;
            }
            Debug.Log( "Player angle is:" + playerAng + " Range of hit angles is:" + minAng + " to " + maxAng +" ,offset is :"+ offset.sqrMagnitude);

            if(minAng > 360)
            {
                minAng -= 360;
            }

            if (maxAng > 360)//if we are looping around 0
            {
                maxAng -= 360;//set the Maximum angle to a small number(below 360).                

            }


            if(minAng >maxAng)//if we are looping around 0
            {               
               if(playerAng > minAng || playerAng < maxAng)
                {//This is OR not AND because of the looping around 0;
                    return true;
                }               
                return false;
                
            }
            else//If we aren't looping around 0
            {//If the player is Greater than the minimum and less than the maximum
                if (playerAng > minAng && playerAng < maxAng)
                {
                    return true;
                }
                return false;
            }

        }
        
        
    }
  

}
