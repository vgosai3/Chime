using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitPointsDisplay : MonoBehaviour
{
    private GameObject player;
    private TMP_Text Text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Text = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Player script = player.GetComponent<Player>();
            Text.SetText("HP: " + script.HitPoints.ToString());
        }
        else
        {
            player = GameObject.FindWithTag("Player");
        }
    }
}
