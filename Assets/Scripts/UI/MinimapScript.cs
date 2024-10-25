using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    private float Height = 10.0f;

    void FixedUpdate()
    {
        transform.position = player.position + new Vector3(0, Height, 0);
        transform.rotation = Quaternion.Euler(90f, Constants.GetCameraAngles().eulerAngles.y, 0f);
    }

}