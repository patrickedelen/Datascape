using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MenuText : MonoBehaviour {

    private Text backgroundText;
    private int updates;
    System.Random random = new System.Random();

	// Use this for initialization
	void Start () {
        backgroundText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        updates++;
        if(updates >= 5)
        {
            updates = 0;

            int randomBinary = random.Next(0, 2);
            backgroundText.text = randomBinary + backgroundText.text;
        }

	
	}
}
