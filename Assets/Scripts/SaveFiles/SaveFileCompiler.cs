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
}
