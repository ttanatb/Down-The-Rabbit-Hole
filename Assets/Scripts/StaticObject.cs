using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour
{
    SceneChange sceneChange;
    // Use this for initialization
    void Start()
    {
        sceneChange = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneChange>();
        Debug.Log(sceneChange.State);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.transform.gameObject.tag == "Player" && gameObject.tag == "Goal")
        {
            Debug.Log("collision");
            sceneChange.ChangeState(SceneChange.SceneState.Win);
        }
        
    }
}