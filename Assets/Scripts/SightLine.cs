using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightLine : MonoBehaviour {
    Vector2 Position;
    Vector2 PlayerPosition;
    Vector2 Offset;
    float OffsetAngle;
    float WidthAngle;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Position = gameObject.GetComponentInParent<Transform>().position;	
        
	}
}
