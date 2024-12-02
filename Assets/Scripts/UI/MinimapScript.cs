using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    protected GameObject _Player;

    [SerializeField]
    private float Height = 10.0f;

    void FixedUpdate()
    {
        _Player = GameObject.FindWithTag("Player");
        transform.position = _Player.transform.position + new Vector3(0, Height, 0);
        transform.rotation = Quaternion.Euler(90f, Constants.GetCameraAngles().eulerAngles.y, 0f);
    }

}