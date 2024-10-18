using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals //Separate class from serializable bc a) it's static and b) might have stuff we don't want to serialize
{

    [SerializeField] public static bool inOrb = false;
    [SerializeField] public static int currentNPC = 0;
    [SerializeField] public static bool isPaused = false;
    [SerializeField] public static bool isDaytime = false;
    [SerializeField] public static long dayCounter = 0;
    [SerializeField] public static float timer = 0;
    [SerializeField] public static Player player = null;
    public static Vector3 playerLocation = Vector3.zero;
    public static int[] items = null;

    public static SerializableDataWatcher saveData = new SerializableDataWatcher();

    public static void SaveFileUpdate() //only to account for other places Globals is used
    {
        saveData.inOrb = inOrb;
        saveData.currentNPC = currentNPC;
        saveData.isPaused = isPaused;
        saveData.isDaytime = isDaytime;
        saveData.dayCounter = dayCounter;
        saveData.playerLocation = player.transform.position;
        saveData.playerInventory = player.getPlayerInventorySerialized();

        SaveFileCompiler.SaveToJson();
    }

    public static void LoadSave() //load all data and update it to globals
    {
        Debug.Log("Loading Save File");
        saveData = SaveFileCompiler.LoadFromJson();
        inOrb = saveData.inOrb;
        currentNPC = saveData.currentNPC;
        isPaused = saveData.isPaused;
        isDaytime = saveData.isDaytime;
        dayCounter = saveData.dayCounter;
        playerLocation = saveData.playerLocation;
        items = saveData.playerInventory;

        Debug.Log("Update :)");
    }

}
