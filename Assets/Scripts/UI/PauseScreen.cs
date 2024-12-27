using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    // Resume the game
    public void Resume()
    {
        Debug.Log("Resuming game...");
        Time.timeScale = 1f;
        Globals.IsPaused = false;
        gameObject.SetActive(false);
    }

    // Load main menu scene
    public void LoadMainMenu()
    {
        Debug.Log("Returning to menu...");
        SceneManager.LoadScene("Main Menu");
    }
}
