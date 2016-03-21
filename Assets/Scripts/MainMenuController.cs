using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


    private GameObject newModal;
    private GameObject continueModal;
    private GameObject creditsModal;
    private GameObject optionsModal;
    private InputField field;

    //save spots
    public Button cb1;
    public Button cb2;
    public Button cb3;
    public Button cb4;
    public Button cb5;
    public Text btx1;
    public Text btx2;
    public Text btx3;
    public Text btx4;
    public Text btx5;



    // Use this for initialization
    void Awake () {
        newModal = GameObject.Find("NewGameModal");
        continueModal = GameObject.Find("ContinueGameModal");
        creditsModal = GameObject.Find("CreditsModal");
        optionsModal = GameObject.Find("OptionsModal");
        field = GameObject.Find("InputField").GetComponent<InputField>();

        newModal.SetActive(false);
        continueModal.SetActive(false);
        creditsModal.SetActive(false);
        optionsModal.SetActive(false);

        PlayerDataIO.Load();
        Debug.Log("Loaded");
        Debug.Log(PlayerDataIO.PlayerDataList.Count);

    }

    public void showNewGame()
    {

        newModal.SetActive(true);

    }
        public void newGameStart()
        {
            PlayerData.current = new PlayerData();
            PlayerData.current.name = field.text;
            Debug.Log(PlayerData.current.name);
            PlayerDataIO.Save();
            SceneManager.LoadScene(2);
        }

    public void showContinue()
    {
        for(int i = 0; i < PlayerDataIO.PlayerDataList.Count; i++)
        {
            if(i == 0)
            {
                btx1.text = PlayerDataIO.PlayerDataList[i].name;
                Debug.Log(PlayerDataIO.PlayerDataList[i].name);
                cb1.onClick.AddListener(() => continuePlayer(PlayerDataIO.PlayerDataList[0]));
            }
            if (i == 1)
            {
                btx2.text = PlayerDataIO.PlayerDataList[i].name;
                cb2.onClick.AddListener(() => continuePlayer(PlayerDataIO.PlayerDataList[1]));
            }
            if (i == 2)
            {
                btx3.text = PlayerDataIO.PlayerDataList[i].name;
                cb3.onClick.AddListener(() => continuePlayer(PlayerDataIO.PlayerDataList[2]));
            }
            if (i == 3)
            {
                btx4.text = PlayerDataIO.PlayerDataList[i].name;
                cb4.onClick.AddListener(() => continuePlayer(PlayerDataIO.PlayerDataList[3]));
            }
            if (i == 4)
            {
                btx5.text = PlayerDataIO.PlayerDataList[i].name;
                cb5.onClick.AddListener(() => continuePlayer(PlayerDataIO.PlayerDataList[4]));
            }

        }
        continueModal.SetActive(true);
    }

    public void continuePlayer(PlayerData pData)
    {

        PlayerData.current = pData;
        SceneManager.LoadScene(2);

    }


    public void showCredits()
    {
        creditsModal.SetActive(true);
    }


    public void showOptions()
    {
        optionsModal.SetActive(true);
    }

    public void dropSaves()
    {
        foreach(PlayerData pd in PlayerDataIO.PlayerDataList)
        {
            PlayerDataIO.PlayerDataList.Remove(pd);
        }
        PlayerDataIO.Save();
        Debug.Log("removed all items from list");

    }



    public void exitGame()
    {
        Application.Quit();
    }

    public void hideAll()
    {
        newModal.SetActive(false);
        continueModal.SetActive(false);
        creditsModal.SetActive(false);
        optionsModal.SetActive(false);
    }
}
