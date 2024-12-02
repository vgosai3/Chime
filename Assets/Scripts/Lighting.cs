using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public float MaxBrightness = 2.0f;

    protected Light DirectionalLight;
    [SerializeField] private static long XRotationLowerBound = 30;
    [SerializeField] private static long XRotationUpperBound = 150;
    [SerializeField] private static float DAY_INTENSITY = 2.5f;
    [SerializeField] private static float NIGHT_INTENSITY = 0.1f; //change as needed, but this works pretty well
    [SerializeField] private static Color LOWER_COLOR = new Color(242f / 255, 146f / 255, 19f / 255);
    [SerializeField] private static Color UPPER_COLOR = new Color(174f / 255, 211f / 255, 242f / 255);
    [SerializeField] private static Color NIGHT_COLOR = new Color(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        DirectionalLight = GetComponent<Light>();
        DirectionalLight.transform.rotation = Quaternion.Euler(XRotationLowerBound, -30, 0);
        DirectionalLight.intensity = DAY_INTENSITY;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = CalculateXAngle();
        DirectionalLight.transform.rotation = Quaternion.Euler(angle, -30, 0);
        DirectionalLight.intensity = Mathf.Min(CalculateIntensity(), MaxBrightness);
        DirectionalLight.color = CalculateColor(angle);
        //Debug.Log("Intensity: " + DirectionalLight.intensity + " Angle: " + angle + "Day: " + Globals.dayCounter + "Color: " + DirectionalLight.color);
    }

    private float CalculateXAngle()
    {
        float timeReference = (Globals.isDaytime) ? Globals.SECONDS_PER_DAY : Globals.SECONDS_PER_NIGHT;
        float timePassedPercentage = Globals.timer / timeReference;
        if (Globals.isDaytime)
        {
            return XRotationLowerBound + (XRotationUpperBound - XRotationLowerBound) * (timePassedPercentage);
        }
        else
        {
            return XRotationUpperBound - (XRotationUpperBound - XRotationLowerBound) * (timePassedPercentage);
        }
    }

    private float CalculateIntensity()
    {
        float totalTime = (Globals.SECONDS_PER_DAY + Globals.SECONDS_PER_NIGHT);
        float timePassed = Globals.timer + totalTime * Globals.dayCounter;

        float functionPosition = Mathf.Sin((((2 * Mathf.PI * timePassed)) / totalTime));
        if (!Globals.isDaytime)
        {
            return NIGHT_INTENSITY; //make night the same consistent darkness
        } else
        {
            return (DAY_INTENSITY - NIGHT_INTENSITY) * Mathf.Pow(functionPosition, 2) + NIGHT_INTENSITY;
        }
        
    }

    private Color CalculateColor(float angle)
    {
        float rotationRange = XRotationUpperBound - XRotationLowerBound;
        Color lower, upper, lightColor;
        if (Globals.isDaytime)
        {
            lower = LOWER_COLOR;
            upper = UPPER_COLOR;
        }
        else
        {
            lower = LOWER_COLOR;
            upper = NIGHT_COLOR;
        }


        Color intensityRange = upper - lower;
        float anglePercentage = ((angle - XRotationLowerBound) / rotationRange);
        float changeIndicator = (XRotationLowerBound + (rotationRange / 2));

        if (angle > changeIndicator) //fade into day
        {
            lightColor = upper - (intensityRange * anglePercentage);
        }
        else //fade into night
        {
            lightColor = lower + (intensityRange * anglePercentage);
        }

        return lightColor;
    }
}