using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startAnimation : MonoBehaviour
{

    private GameObject canvasMain, cameraMain, canvasBlack;
    private int updateCount;
    private CanvasGroup cv;
    private CanvasGroup cvb;

    void Awake()
    {
        canvasMain = GameObject.Find("Canvas");
        cameraMain = GameObject.Find("Camera");
        canvasBlack = GameObject.Find("CanvasBlack");
        cv = canvasMain.GetComponent<CanvasGroup>();
        cvb = canvasBlack.GetComponent<CanvasGroup>();
    }
    void Start()
    {
        canvasMain.SetActive(true);
        cameraMain.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateCount++;
        if (updateCount > 200 && cv.alpha > 0)
        {
            cv.alpha -= .05f;
            if(cv.alpha == 0)
                canvasMain.SetActive(false);
                cameraMain.SetActive(true);
        } else if(updateCount > 750 && cvb.alpha < 1)
        {
            cvb.alpha += .05f;
        } else if(cvb.alpha == 1)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
;