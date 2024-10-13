using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDataWatcher
{
    [SerializeField] public bool inOrb = false;
    [SerializeField] public int currentNPC = 0;
    [SerializeField] public bool isPaused = false;
    [SerializeField] public bool isDaytime = false;
    [SerializeField] public long dayCounter = 0;
    [SerializeField] public Vector3 playerLocation = Vector3.forward;
    [SerializeField] public int[] playerInventory = null;
}
