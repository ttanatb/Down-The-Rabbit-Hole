using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    enum SceneState
    {
        Play, Win, Pause, Lose
    }

    SceneState state;

	// Use this for initialization
	void Start () {
        state = SceneState.Play;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeState(SceneState state)
    {
        if(state == this.state)
        {
            return;
        }else if(state == SceneState.Win){
            Application.LoadLevel("");
        }

    }


}
