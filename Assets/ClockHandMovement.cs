using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f,
            Globals.isDaytime
            ? (Globals.timer % Globals.SECONDS_PER_DAY) / Globals.SECONDS_PER_DAY * -180
            : (Globals.timer % Globals.SECONDS_PER_NIGHT) / Globals.SECONDS_PER_NIGHT * -180 - 180);
    }
}
