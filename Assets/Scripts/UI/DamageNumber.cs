using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    // Set time 
    public float timeAlive = 0.5f;
    public float Delta = 0.1f;
    public float Damage = 1.0f;
    private float _spawnTime;

    private TextMeshPro Text;


    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = Time.time;
        Text = gameObject.GetComponent<TextMeshPro>();
        Text.SetText(Damage.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive < Time.time - _spawnTime)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.up * Delta * Time.deltaTime);
        Text.alpha = (timeAlive - (Time.time - _spawnTime)) / timeAlive;
    }
}
