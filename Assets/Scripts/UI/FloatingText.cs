using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Set time 
    public float timeAlive = 0.5f;
    public float Delta = 0.1f;
    public string Text = "";
    public float FontSize = 8.0f;
    private float _spawnTime;

    public TextMeshPro TMS;


    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = Time.time;
        TMS = gameObject.GetComponent<TextMeshPro>();
        TMS.SetText(Text);
        TMS.fontSize = FontSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive < Time.time - _spawnTime)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.up * Delta * Time.deltaTime);
        TMS.alpha = (timeAlive - (Time.time - _spawnTime)) / timeAlive;
    }
}
