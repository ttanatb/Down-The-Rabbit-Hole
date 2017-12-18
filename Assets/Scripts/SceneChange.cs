using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    private PlayerController player;
    int levelCount;
    int maxLevels;
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

        levelCount = SceneManager.GetActiveScene().buildIndex;
        maxLevels = SceneManager.sceneCount - 1;
        if (state == SceneState.Play||state == SceneState.MainMenu)
        {
            menuManager = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>();
        }

        player = FindObjectOfType<PlayerController>();
	}

    // Update is called once per frame
    void Update()
    {
        //currently empty  
        
        
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
            state = SceneState.Win;
            SceneManager.LoadScene("Win_Screne");
        }
        else if (state == SceneState.Lose)
        {
            SceneManager.LoadScene("Lose_Screne");
            state = SceneState.Lose;
        }
        else if (state == SceneState.Play)
        {
            SceneManager.LoadScene(0);
            state = SceneState.Play;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(levelCount + 1);
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

    public void IncrementLevel()
    {
        SightLine.IsPaused = true;
        if (!player)
            player = FindObjectOfType<PlayerController>();

        player.Win();

        Invoke("LoadNextScene", 1f);
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt("colLevel" + LevelCount, 0);
        if (!player)
            player = FindObjectOfType<PlayerController>();
        player.Die();
        Invoke("ReloadScene", 1f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(LevelCount);
    }


    /// <summary>
    /// Returns current state
    /// </summary>
    public SceneState State
    {
        get { return state; }
     
    }

    public int LevelCount
    {
        get { return levelCount; }
    }
    public int MaxLevels
    {
        get { return maxLevels; }
    }
}
