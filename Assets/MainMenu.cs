using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Play button functionality take to level 1
    public void PlayGame() 
    {
        SceneManager.LoadSceneAsync(0);
    }   

    //Quit button functionality
    public void QuitGame() {
        Application.Quit();
    }
}