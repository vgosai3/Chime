using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenGUI : MonoBehaviour
{
    [SerializeField] GameObject DeathScreen; 

    // Might need to make a UI manager
    private void Start()
    {
        DeathScreen.SetActive(false);
    }

    // Pause game on death
    public void ShowDeathScreen()
    {
        Time.timeScale = 0f; 
        DeathScreen.SetActive(true);
        
    }
}

