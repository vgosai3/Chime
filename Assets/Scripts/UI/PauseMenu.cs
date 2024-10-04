using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Globals.isPaused)
            {
                Resume();
            } else 
            {
                Pause();
            }
        } 
    }

    //Access both methods from buttons
    public void Resume()
    {
        Debug.Log("Resuming game...");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Globals.isPaused = false;
    }

    //If we want to add a button for pause,
    //use this method here
    public void Pause()
    {
        Debug.Log("Pausing game...");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Globals.isPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Returning to menu...");
        SceneManager.LoadScene("Main Menu");

    }
}
