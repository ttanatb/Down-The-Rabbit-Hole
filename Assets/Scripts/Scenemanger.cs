using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenemanger : MonoBehaviour {

    enum SceneState
    {
        LevelOne,
        WinScrene,
        LoseScrene
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StateChange(SceneState state)
    {
        if(state == SceneState.LevelOne)
        {
            ///Application.LoadLevel()
        }else if(state == SceneState.WinScrene)
        {
            Scenemanger.
        }else if(state == SceneState.LoseScrene)
        {

        }
    }

}
