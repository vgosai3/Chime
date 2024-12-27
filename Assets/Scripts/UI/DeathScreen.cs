using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{

    // Might need to make a UI manager
    private void Start()
    {
        gameObject.SetActive(false);
        Globals.Player.OnDeath += DeathScreenHandler;
    }

    // Pause game on death
    public void DeathScreenHandler(object sender, bool isDead)
    {
        if (isDead)
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }
    }
}

