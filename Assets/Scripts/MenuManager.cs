using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GUISkin myGUI;

    void OnGUI()
    {
        GUI.skin = myGUI;
    }

}
