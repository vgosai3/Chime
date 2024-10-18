using System;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime;

    [SerializeField]
    private double radius = 16;
    /// <summary>
    /// Distance of the camera from the player.
    /// </summary>
    public double Radius
    {
        get { return radius; }
        set {
            radius = value;
            UpdateOffset();
        }
    }

    [SerializeField]
    private double horizontalAngle = 45;
    /// <summary>
    /// Angle of the camera relative to the y-axis, in degrees.
    /// </summary>
    public double HorizontalAngle
    {
        get { return horizontalAngle; }
        set
        {
            horizontalAngle = value;
            UpdateOffset();
        }
    }

    [SerializeField]
    private double verticalAngle = 45;
    /// <summary>
    /// Angle of the camera relative to the x-axis, in degrees.
    /// </summary>
    public double VerticalAngle
    {
        get { return verticalAngle; }
        set
        {
            verticalAngle = value;
            UpdateOffset();
        }
    }

    private Vector3 Offset;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        UpdateOffset();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

    public void UpdateOffset()
    {
        // Set camera size to match radius
        GetComponent<Camera>().orthographicSize = (float) Radius;
        // Change clipping planes to prevent object clipping near edge of camera
        // GetComponent<Camera>().nearClipPlane = (float) -Radius;
        // GetComponent<Camera>().farClipPlane = (float) Radius;
        // Change camera rotation to specified angles
        transform.rotation = Quaternion.Euler((float) VerticalAngle, (float) HorizontalAngle, 0.0f);
        // Calculate angles in radians
        var hRad = HorizontalAngle * Math.PI / 180;
        var vRad = (90 - VerticalAngle) * Math.PI / 180;
        // Calculate the camera offset
        Offset = new(
            (float)(-Radius * Math.Sin(vRad) * Math.Cos(hRad)),
            (float)(Radius * Math.Cos(vRad)),
            (float)(-Radius * Math.Sin(vRad) * Math.Sin(hRad)));
    }
}
