using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour
{
    SceneChange sceneChange;
    // Use this for initialization
    void Start()
    {
        sceneChange = GameObject.Find("SceneManager").GetComponent<SceneChange>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"&& gameObject.tag =="Goal")
            sceneChange.ChangeState(SceneChange.SceneState.Win);
        
    }
}