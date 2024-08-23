using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryOrbDestination : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerq;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            playerq.SetActive(false);
            player.position = destination.position;
            playerq.SetActive(true);
        }
    }
    
}
