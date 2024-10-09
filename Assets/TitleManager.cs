using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void ClearData()
    {
        string path = Application.persistentDataPath + "/player-stats.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void QuitToDesktop()
    {
        Debug.Log("!!! QUIT CALLED !!!");
        Application.Quit();
    }
}
