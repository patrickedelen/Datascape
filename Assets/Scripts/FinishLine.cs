using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    private GameObject car;
    private LevelController LC;

    void Start()
    {
        car = GameObject.FindWithTag("Player");
        LC = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject == car) && (LC.DataCount == LC.DataRequired))
        {
            LC.GameState = 1;
            LC.GameOver();

        }
    }

}
