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
        Debug.Log(sceneChange.State);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        Debug.Log("collision");
        if (collision.gameObject.tag == "Player"&& gameObject.tag =="Goal")
            sceneChange.ChangeState(SceneChange.SceneState.Win);
        
    }
}