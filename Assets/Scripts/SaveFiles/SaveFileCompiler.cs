using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveFileCompiler
{

    public static void SaveToJson()
    {

        string playerData = JsonUtility.ToJson(Globals.saveData);
        string filePath = Application.dataPath + "/InventoryData.json";
        //Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, playerData);
        //Debug.Log("Saved!");
    }

    public static SerializableDataWatcher LoadFromJson()
    {
        string filePath = Application.dataPath + "/InventoryData.json";
        Debug.Log(filePath);
        string playerData = System.IO.File.ReadAllText(filePath);
        Debug.Log("Saved!");
        return JsonUtility.FromJson<SerializableDataWatcher>(playerData);
    }
}
