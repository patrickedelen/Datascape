using UnityEngine;
using System.Collections;

public class PoliceSight : MonoBehaviour {

    public float policeFOV = 120f;
    public bool playerSighted;
    public Vector3 policeLastSight;
    public bool sightBeforeData;

    private NavMeshAgent nav;
    private SphereCollider sp;
    private LevelController LC;
    private GameObject player;
    private Material gray;
    private Vector3 pastSight;
    private float playerHackTime;


	void Awake () {
        nav = GetComponent<NavMeshAgent>();
        sp = GetComponent<SphereCollider>();
        LC = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        player = GameObject.FindWithTag("Player");
        gray = new Material(Shader.Find("Hidden/Grayscale Effect"));

        pastSight = LC.FallbackPlayerPosition;
        policeLastSight = LC.FallbackPlayerPosition;
    }
	
	// Update is called once per frame
	void Update () {

        if(LC.GlobalPlayerPosition != pastSight)
        {
            policeLastSight.Set(LC.GlobalPlayerPosition.x, LC.GlobalPlayerPosition.y, LC.GlobalPlayerPosition.z);
        }

        pastSight.Set(LC.GlobalPlayerPosition.x, LC.GlobalPlayerPosition.y, LC.GlobalPlayerPosition.z);

	}

    //If sphere collides
    void OnTriggerStay (Collider col)
    {
        if((col.gameObject == player) && (sightBeforeData || (LC.DataCount > 0) || LC.TooFast))
        {
            playerSighted = false;

            //check that the player is within the FOV
            Vector3 playerV = col.transform.position - transform.position;
            float angle = Vector3.Angle(playerV, transform.forward);

            //check if angle is 1/2 fov
            if(angle < (policeFOV * .5))
            {

                playerSighted = true;
                LC.undetected = false;
                //set the global
                LC.GlobalPlayerPosition.Set(player.transform.position.x, player.transform.position.y, player.transform.position.z);


                if (playerV.magnitude < 8f)
                {
                    playerHackTime += .25f;
                    LC.DangerZone = true;
                    Debug.Log(playerHackTime);

                    //If the player is in range for 5 secs they lose
                    if (playerHackTime >= 5f)
                    {
                        Debug.Log("Police have hacked you");
                        LC.GameState = 2;
                        LC.GameOver();
                    }

                }
                else
                {
                    playerHackTime = 0;
                    LC.DangerZone = false;
                }

            }



        }
    }

    //if the col exits
    void OnTriggerExit (Collider col)
    {
        if (col.gameObject == player)
        {
            playerSighted = false;
            playerHackTime = 0;
            LC.DangerZone = false;
        }
    }
}
