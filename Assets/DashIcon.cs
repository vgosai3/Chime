using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashIcon : MonoBehaviour
{
    /// <summary>
    /// The Player object.
    /// </summary>
    protected GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovementComponent script = _Player.GetComponent<CharacterMovementComponent>();
        GetComponent<Image>().fillAmount = Math.Min((Time.time - script.LastDash) / script.DashCooldown, 1.0f);

    }
}
