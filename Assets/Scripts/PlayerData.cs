using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerData {

    //current playerdata refrence
    public static PlayerData current;
    public string name;
    public int[] score;

    public PlayerData()
    {
        score = new int[10];
    }

}
