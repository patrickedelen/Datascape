using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private GameObject guiTextObject;
    private Text hintText;
    private string[] guiMessage = {"Welcome to Datascape!", "Use W, A, S, and D to move...", "And escape to pause.", "Drive to the blue data particles", "", "When you pick them up, the red finish pad will change color", "You can finish the level by driving over it" };
    private float[] messageTime = { 2f, 4f, 3f, 4f, 3f, 4f, 3f };
    private int currentMessage;
    private bool guiShow;

	// Start showing hint text

	void Start () {
        hintText = GameObject.Find("HintText").GetComponent<Text>();
        MessageManager(0);
        

    }
	
    void MessageManager(int index)
    {
        StartCoroutine(ShowMessage(index));
    }

    IEnumerator ShowMessage(int index)
    {
        hintText.text = guiMessage[index];
        yield return new WaitForSeconds(messageTime[index]);
        index++;
        if(index < messageTime.Length)
            MessageManager(index);

    }
}

