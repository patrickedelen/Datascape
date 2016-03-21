using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class menuButton : MonoBehaviour {


    public Text score;
    public int scoreInt;
    public Text messageText;
    public int levelInt;
    public GameObject playButton;

    public Button b0;

    private string[] text = new string[11] {
    "Long ago we lived in a time of peace, of freedom. No one thought for us; decided what was right and wrong for us. Not now. The data police have taken that away. They watch everything we do, but I’ve found a way to stop them, and I need your help. Race through the city and pick up data for me and together we can end the reign of censorship.",
    "Your first task is to learn your car. I've prepared a short track for you to get a feel for the controls. The blue particles are data; we will need lots of them to take back the city. When you collect the data you will be permitted to leave the level.",
    "Pretty easy, right? This next trial is a real course, with real data police. You must avoid them at all costs. As long as you don't have the data they won't mess with you, but once you pick it up, MOVE! If you go too fast, however, they'll take notice.",
    "Nice job! Your minimap will show you the road in most courses, and changes color based on danger. Black means you're going fast enough for the data police to notice. Red means drive now! This next course takes place in the suburbs, take the first left and keep watch.",
    "Your fourth mission takes place in the virtual world. Collect the two pieces of data and we'll be even closer to our goal!",
    "Excellent! It's almost time to take on the city. Drive along this highway and pick up the piece of data you find. It'll help us later.",
    "Now that we're in the city, we need to be careful. Mision 6 is in the sewers. Collect the data here and we will move back above ground. ",
    "The city outskirts await us. There are two pieces of data hidden in this map, along with even stronger data police. Be careful.",
    "We need one more trip to the virtual world. This level has three pieces of critical data for our final quest. Watch out for the data police - they are known to patrol the virtual world as much as the physical one.",
    "Finally, the city awaits us. Collect the two pieces of data in the heart of the city and we will finally have enough power to overthrow the data police and restore power to the people.",
    "The time has come! No need to return home this time, drive as fast as you can to the heart of the city. One data pad waits for you. When you reach it, all the information we have collected will overwhem the data police network and free the people!"
    };

    public void Awake()
    {
        scoreInt = PlayerData.current.score.Sum();
        string scoreString = scoreInt.ToString();
        score.text = scoreString;
        playButton.SetActive(false);
    }

    public void OnClick(int level)
    {

        SceneManager.LoadScene(level);
    }

    public void showText(int buttonIndex)
    {

        levelInt = buttonIndex + 2;
        messageText.text = text[buttonIndex];
        if (buttonIndex != 0)
        {
            playButton.SetActive(true);
        } else
        {
            playButton.SetActive(false);
        }
    }
    public void changeSceneSelected()
    {
        SceneManager.LoadScene(levelInt);
    }

}
