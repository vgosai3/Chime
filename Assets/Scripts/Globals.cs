using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Globals //Separate class from serializable bc a) it's static and b) might have stuff we don't want to serialize
{

    [SerializeField] public static bool inOrb = false;
    [SerializeField] public static int currentNPC = 0;
    [SerializeField] public static bool IsPaused = false;
    [SerializeField] public static bool isDaytime = true;
    [SerializeField] public static long dayCounter = 0;
    [SerializeField] public static float timer = 0;

    // Timing of Days
    [SerializeField] public static long SECONDS_PER_DAY = 30; // should be static but
    [SerializeField] public static long SECONDS_PER_NIGHT = 30; // playtest first

    public static Player Player = null;
    public static Vector3 playerLocation = Vector3.zero;
    public static int[] items = null;

    public static SerializableDataWatcher saveData = new SerializableDataWatcher();

    public static void Start()
    {
        SceneManager.activeSceneChanged += PlayerUpdateHandler;
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private static void PlayerUpdateHandler(Scene current, Scene next)
    {
        var sceneObjects = next.GetRootGameObjects();
        foreach (var sceneObject in sceneObjects)
        {
            if (sceneObject.CompareTag("Player")) {
                Player = sceneObject.GetComponent<Player>();
                break;
            }
        }
    }

    private static void UpdatePlayer()
    {

    }

    public static void SaveFileUpdate() //only to account for other places Globals is used
    {
        saveData.inOrb = inOrb;
        saveData.currentNPC = currentNPC;
        saveData.isPaused = IsPaused;
        saveData.isDaytime = isDaytime;
        saveData.dayCounter = dayCounter;
        saveData.playerLocation = Player.transform.position;
        // saveData.playerInventory = Player.getPlayerInventorySerialized();

        SaveFileCompiler.SaveToJson();
    }

    public static void LoadSave() //load all data and update it to globals
    {
        Debug.Log("Loading Save File");
        saveData = SaveFileCompiler.LoadFromJson();
        inOrb = saveData.inOrb;
        currentNPC = saveData.currentNPC;
        IsPaused = saveData.isPaused;
        isDaytime = saveData.isDaytime;
        dayCounter = saveData.dayCounter;
        playerLocation = saveData.playerLocation;
        items = saveData.playerInventory;

        Debug.Log("Update :)");
    }

}
