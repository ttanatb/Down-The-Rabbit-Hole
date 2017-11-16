using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SightLine : MonoBehaviour {
    Vector3 position;
    Vector3 playerPosition;
    Vector3 offset;
    Vector3 leftPos, rightPos;
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
        sceneChange = GameObject.Find("SceneManager").GetComponent<SceneChange>();
        myRenderer = gameObject.GetComponent<LineRenderer>();
 
	}
	
	// Update is called once per frame
	void Update () {
        position = gameObject.transform.position;
        playerPosition = Player.transform.position;
        offset = playerPosition - position;
        offsetAngle = gameObject.transform.rotation.eulerAngles.z;
     

        maxAng = 90 + offsetAngle + (widthAngle / 2);//Far angle           
        minAng = 90 + (offsetAngle - (widthAngle / 2));

        float maxAngRad = maxAng * Mathf.Deg2Rad;
        float minAngRad = minAng * Mathf.Deg2Rad;


        leftPos.x = position.x + Radius * (Mathf.Cos(maxAngRad));
        leftPos.y = position.y + Radius * ( Mathf.Sin(maxAngRad));

        rightPos.x = position.x + Radius * (Mathf.Cos(minAngRad));
        rightPos.y = position.y + Radius * ( Mathf.Sin(minAngRad));
        myRenderer.SetPosition(0, position);
        myRenderer.SetPosition(1, leftPos);
        myRenderer.SetPosition(2, rightPos);

        
        if (offset.sqrMagnitude < Radius*Radius)//Circle Collision Check;
        {
            if (SightLineCheck())
            {                
                Debug.Log("HIT");
                //sceneChange.ChangeState(SceneChange.SceneState.Lose);
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



            if (maxAng > 360)//if we are looping around 0
            {
                maxAng -= 360;//set the Maximum angle to a small number(below 360).
                //Player is greater than the minimum, or less than the maximum;
                if (playerAng > minAng || playerAng < maxAng)
                {//This is OR not AND because of the looping around 0;
                    return true;
                }
                return false;

            }
            else if(minAng < 0)//if we are looping around 0
            {
                minAng += 360;//set the Maximum angle to a small number(below 360).
                //Player is greater than the minimum, or less than the maximum;
                if(playerAng < minAng || playerAng > maxAng)
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
