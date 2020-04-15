using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static Dictionary<string, GameObject[]> Data = new Dictionary<string, GameObject[]>();

    public static void Init()
    {
        Data["Field"] = GameObject.FindGameObjectsWithTag("Field");
        Data["Home"] = GameObject.FindGameObjectsWithTag("Home");
        Data["Finish"] = GameObject.FindGameObjectsWithTag("Finish");
    }
}