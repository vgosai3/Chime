using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject player;
    private Image Health_Bar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Image = gameObject.GetComponent<Health_Bar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Player script = player.GetComponent<Player>();
            Health_Bar.fillamount = script.HitPoints/100f;
        }
        else
        {
            player = GameObject.FindWithTag("Player");
        }
    }

}


