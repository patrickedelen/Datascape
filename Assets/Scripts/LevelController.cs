using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    //setup stuff
    private int levelIndex;

    //Audio stuff
    private int[] levelMusic = new int[10]{ 0, 1, 2, 3, 1, 0, 3, 2, 4, 5 };
    private AudioController audioObj;

    //Game variables//
    public int GameState;
    public int SecondsRemaning;
    //Multiple data pickup count
    public int DataCount;
    public int DataRequired;
    public bool DangerZone;
    public bool TooFast;
    //stealth bonus boolean
    public bool undetected;
    //Player locations
    private GameObject player;
    private Car playerCar;
    public Vector3 GlobalPlayerPosition;
    public Vector3 FallbackPlayerPosition;

    //camera
    public Camera miniMap;

    //black rectangle
    private CanvasGroup blackC;
    private int fadeState; //0: not fading, 1: fading in, 2: fading to black
    //Text for score display
    private Text textScore;


    //Instantiate all the important stuff
    void Awake () {

        //Paint black rectangle for fade in and out
        blackC = GameObject.Find("BlackCanvas").GetComponent<CanvasGroup>();
        textScore = GameObject.Find("TextScore").GetComponent<Text>();
        fadeState = 1;

        audioObj = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();

        

        //Start the timer countdown
        Debug.Log("Started SecondsRemaining");
        InvokeRepeating("UpdateTimer", 1, 1);
        SecondsRemaning = 100;

        //Set the gamestate
        GameState = 0;
        DangerZone = false;
        undetected = true;
        DataCount = 0;
        GameObject[] dataCountArr = GameObject.FindGameObjectsWithTag("Data");
        DataRequired = dataCountArr.Length;
        Debug.Log(DataRequired);


        //find the player
        player = GameObject.FindGameObjectWithTag("Player");
        playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<Car>();
        miniMap = GameObject.Find("MinimapCamera").GetComponent<Camera>();

        //set the level index for scoring
        levelIndex = (SceneManager.GetActiveScene().buildIndex - 3);
        //play some music
        audioObj.LevelMusic(levelMusic[levelIndex]);
        if (levelIndex == 5)
        {
            SecondsRemaning += 60;
        }
        if (levelIndex == 8 || levelIndex == 9)
        {
            SecondsRemaning += 100;
        }



    }

    //Pause menu functions//
    //Restart Level
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {

            if (DangerZone)
            {
                miniMap.backgroundColor = Color.red;

            }else if (TooFast)
            {
                miniMap.backgroundColor = Color.black;
            }else
            {
                miniMap.backgroundColor = Color.gray;
            }

    }
    
    //fixed update for fading
    void FixedUpdate()
    {
        if (fadeState != 0)
        {
            if (fadeState == 1)
            {
                blackC.alpha -= .02f;

                if(blackC.alpha <= .05f)
                {
                    blackC.alpha = 0f;
                    fadeState = 0;
                }
            }

            if (fadeState == 2)
            {
                blackC.alpha += .02f;

                if (blackC.alpha >= .95f)
                {
                    fadeState = 0;
                }
            }
        }
    }

    //fixed score time update every 1 sec
    void UpdateTimer()
    {
        SecondsRemaning--;
        Debug.Log(SecondsRemaning);
    }

    //Called when the game is over
    public void GameOver()
    {
        //Stop the timer countdown
        CancelInvoke("UpdateTimer");
        Debug.Log("Game over. Game State: " + GameState);

        fadeState = 2;
        if(GameState == 1)
        {
            int score = SecondsRemaning;
            if(score < 10)
            {
                score = 10;
            }
            if (undetected)
            {
                score += 25;
            }
            Debug.Log("Total score: " + score);

            textScore.text = "You win!\nYour score was: " + score;

            PlayerData.current.score[levelIndex] = score;
            PlayerDataIO.Save();

        }else if(GameState == 2)
        {
            Debug.Log("You lose");
            textScore.text = "Game over. Try again?";
        }

        StartCoroutine(sceneDelay(2.5f));
        

    }

    //wait before changing scene
    IEnumerator sceneDelay(float seconds)
    {
        Debug.Log("waiting for seconds: " + seconds);
        yield return new WaitForSeconds(seconds);
        ChangeLevel(2);
    }

    //Called to change the scene
    public void ChangeLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void MenuTransitionLoad(int level)
    {
        Debug.Log("Transition");
        ChangeLevel(level);
    }

    public void LevelTransitionLoad(int level)
    {
        Debug.Log("Transition");
        ChangeLevel(level);
    }
}
