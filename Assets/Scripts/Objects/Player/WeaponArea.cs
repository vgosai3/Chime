using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArea : MonoBehaviour
{
    public Transform target;
    private readonly Vector3 offset = new(-1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + (target.rotation.normalized * Quaternion.Euler(0.0f, 0.0f, 0.0f)) * offset;
        transform.rotation = target.rotation * Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }
}
