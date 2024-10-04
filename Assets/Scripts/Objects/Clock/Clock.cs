using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

CLOCK CLASS USAGE
- Add Clock script to one singular Clock Object
- Check the [SerializeField] variables (also seen in the Clock Object) 
    and adjust them for testing
- Getters: Day(), Timer(), Paused() or IsPaused()
- Pause() to pause clock
- UnPause() to unpause clock
- Note: there is only a day counter,
    so use ["Day:" + Clock.Day()] and ["Night:" + Clock.Day()]. 
    Therefore, if it is Day 2 and it just became nighttime, it would be Night 2.

*/

public class Clock : MonoBehaviour
{
    // Timing of Days
    [SerializeField] private long SECONDS_PER_DAY   = 100; // should be static but
    [SerializeField] private long SECONDS_PER_NIGHT = 100; // playtest first   

    // Pausing
    public void Pause()
    {
        Globals.isPaused = true;
    }

    public void UnPause()
    {
        Globals.isPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Globals.isPaused)
        {
            UpdateTime();
        }

    }

    void UpdateTime()
    {
        Globals.timer += Time.deltaTime;

        // Switch Day/Night if timer has reached the end
        if (Globals.saveData.isDaytime) 
        {
            // Switch to Night if Day ended
            bool dayEnd = Globals.timer >= SECONDS_PER_DAY;
            if (dayEnd)
            {
                // Change to night, Reset timer
                // DO NOT increment day because night has not ended.
                Globals.saveData.isDaytime = false;
                Globals.timer = 0;
            }
        } else 
        {
            // Switch to Day if Night ended
            bool nightEnd = Globals.timer >= SECONDS_PER_NIGHT;
            if (nightEnd)
            {
                // Change to day, Increment day, and Reset timer
                Globals.saveData.isDaytime = true;
                Globals.dayCounter++;
                Globals.timer = 0;
            }
        }

    }
}
