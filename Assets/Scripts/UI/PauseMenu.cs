using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        isPaused = false;
    }

    //If we want to add a button for pause,
    //use this method here
    public void Pause()
    {
        Debug.Log("Pausing game...");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Returning to menu...");
        SceneManager.LoadScene("Main Menu");

    }
}
