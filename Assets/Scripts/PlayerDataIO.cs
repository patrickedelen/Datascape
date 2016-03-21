using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class PlayerDataIO {

    //list of all playerdata objects
    public static List<PlayerData> PlayerDataList = new List<PlayerData>();

    //called every time a save of player data is required
    public static void Save()
    {
        PlayerDataIO.PlayerDataList.Add(PlayerData.current);
        BinaryFormatter bi = new BinaryFormatter();
        Debug.Log("Saving game to " + Application.dataPath + "/playerData.sf"); //.sf as it's a save file
        FileStream fOut = File.Create(Application.dataPath + "/playerData.sf");
        bi.Serialize(fOut, PlayerDataIO.PlayerDataList);
        fOut.Close();
        //file saved
    }

    //Called when main menu starts to load player data
    public static void Load()
    {
        if(File.Exists(Application.dataPath + "/playerData.sf"))
        {
            BinaryFormatter bi = new BinaryFormatter();
            FileStream fIn = File.Open(Application.dataPath + "/playerData.sf", FileMode.Open);
            PlayerDataIO.PlayerDataList = (List<PlayerData>)bi.Deserialize(fIn);
            fIn.Close();
        } else
        {
            //throw FileNotFoundException;
        }
    }
}
