using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
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
        Player script = _Player.GetComponent<Player>();
        GetComponent<Image>().fillAmount = script.HitPoints / script.MaxHitPoints;

    }
}
