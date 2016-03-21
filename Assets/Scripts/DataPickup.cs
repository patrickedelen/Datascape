using UnityEngine;
using System.Collections;

public class DataPickup : MonoBehaviour {

    private GameObject player;
    private LevelController LC;

    private Light finishLight;
    private ParticleSystem finishParticles;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        LC = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        finishLight = GameObject.Find("FinishLight").GetComponent<Light>();
        finishParticles = GameObject.Find("FinishPad").GetComponent<ParticleSystem>();

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            Debug.Log("Data picked up");
            LC.GlobalPlayerPosition.Set(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            Destroy(gameObject);
            LC.DataCount++;

            finishLight.color = Color.green;
            finishParticles.startColor = Color.green;
        }
    }
}
