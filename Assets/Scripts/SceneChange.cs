using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public enum SceneState
    {
         Play, Win, Lose, MainMenu
    }

    SceneState state;
    MenuManager menuManager;
    void Awake()
    {
        state = CheckState();
    }
    // Use this for initialization
    void Start () {
        
        
        if (state == SceneState.Play||state == SceneState.MainMenu)
        {
            menuManager = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>();
        }
	}

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(state);
        //checking if the player wants to play again here
        if ((state == SceneState.Win|| state == SceneState.Lose) && Input.GetKeyUp(KeyCode.Space))
                ChangeState(SceneState.Play);
       
        
        
    }
    
	
    /// <summary>
    /// Changes the state
    /// </summary>
    /// <param name="state">The scene state to pass in</param>
     public void ChangeState(SceneState state)
    {
        //checking to see if we're already
        //in the scene we're changing to 
        if (state == this.state)
            return;   //do nothing
        else if (state == SceneState.Win)
        {
            SceneManager.LoadScene("Win_Screne");
            state = SceneState.Win;
        }
        else if (state == SceneState.Lose)
        {
            SceneManager.LoadScene("Lose_Screne");
            state = SceneState.Lose;
        }
        else if (state == SceneState.Play)
        {
            SceneManager.LoadScene("W2Sprint");
            state = SceneState.Play;
        }

        

    }
    /// <summary>
    /// Helper method for intialization
    /// </summary>
    /// <returns></returns>
    SceneState CheckState()
    {
        if (SceneManager.GetActiveScene().name == "Win_Screne")
            return SceneState.Win;
        else if (SceneManager.GetActiveScene().name == "Lose_Screne")
            return SceneState.Lose;
        else
            return SceneState.Play;

    }
    /// <summary>
    /// Returns current state
    /// </summary>
   public SceneState State
    {
        get { return state; }
     
    }


}
