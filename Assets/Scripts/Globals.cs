using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour //Separate class from serializable bc a) it's static and b) might have stuff we don't want to serialize
{

    [SerializeField] public static bool inOrb = false;
    [SerializeField] public static int currentNPC = 0;
    [SerializeField] public static bool isPaused = false;
    [SerializeField] public static bool isDaytime = false;
    [SerializeField] public static long dayCounter = 0;
    [SerializeField] public static float timer = 0;
    [SerializeField] public static Player player = null;
    public static SerializableDataWatcher saveData = new SerializableDataWatcher();

    public static void SaveFileUpdate() //only to account for other places Globals is used
    {
        saveData.inOrb = inOrb;
        saveData.currentNPC = currentNPC;
        saveData.isPaused = isPaused;
        saveData.isDaytime = isDaytime;
        saveData.dayCounter = dayCounter;
        saveData.playerLocation = player.transform.position;
        saveData.playerInventory = player.getPlayerInventory();
        Debug.Log(player.transform.position);

        SaveFileCompiler.SaveToJson();
    }

}
