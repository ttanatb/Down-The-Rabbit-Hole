using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour
{
    SceneChange sceneChange;
    AudioSource collectSrc;

    // Use this for initialization
    void Start()
    {
        sceneChange = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneChange>();
        collectSrc = GetComponent<AudioSource>();
        //Debug.Log(sceneChange.State);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        //Debug.Log("collision");
        if (collision.transform.gameObject.tag == "Player" && gameObject.tag == "Goal")
        {
            //if the current level plus one equals the maximum amount of levels
            if(sceneChange.LevelCount + 1 == sceneChange.MaxLevels)
            {
                sceneChange.ChangeState(SceneChange.SceneState.Win);
            }
            else
            {
                //otherwise go to the next level;
                PlayerTimer plaTimer = collision.GetComponentInParent<PlayerTimer>();
                plaTimer.End(sceneChange.LevelCount);
                sceneChange.IncrementLevel();
               
            }
        }

        else if (collision.transform.gameObject.tag == "Player" && gameObject.tag == "Collectable")
        {
            PlayerPrefs.SetInt("colLevel" + sceneChange.LevelCount, 1);//Change colLevel(1) to true, to signifify that the collectable has been collected.

            Collider2D myCollider = gameObject.GetComponentInParent<Collider2D>();
            myCollider.enabled = false;
            SpriteRenderer myRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
            myRenderer.enabled = false;
            collectSrc.Play();
        }

    }
}