using UnityEngine;
using System.Collections;

public class PoliceMove : MonoBehaviour {

    public float PursueSpeed = 15f;
    public float PatrolSpeed = 10f;
    public float PursueWait = 4f;
    public float PatrolWait = 1f;
    public Transform[] patrolWayPoints;

    private PoliceSight policeSight;
    private NavMeshAgent nav;
    private LevelController LC;
    private GameObject player;
    private float pursueTime;
    private float patrolTime;
    private int wayPointIndex;


    void Awake ()
    {
        policeSight = GetComponent<PoliceSight>();
        nav = GetComponent<NavMeshAgent>();
        LC = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        player = GameObject.FindWithTag("Player");

    }

	
	// Update is called once per frame
	void Update () {
	    if(policeSight.policeLastSight != LC.FallbackPlayerPosition && LC.GameState == 0)
        {
            Pursue();
            //Debug.Log("Pursuing");
        } else
        {
            Patrol();
            //Debug.Log("Patroling");
        }
	}

    //pursue the player
    void Pursue()
    {
        //set nav to pursue the player
        nav.speed = PursueSpeed;
        nav.destination = policeSight.policeLastSight;


    }

    //patrol the waypoints
    void Patrol()
    {
        nav.speed = PatrolSpeed;

        if(nav.destination == LC.FallbackPlayerPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTime += Time.deltaTime;

            if(patrolTime >= PatrolWait)
            {
                if(wayPointIndex == patrolWayPoints.Length - 1)
                {
                    wayPointIndex = 0;
                } else
                {
                    wayPointIndex++;
                }

                patrolTime = 0;

            }
        } else
        {
            patrolTime = 0;
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;

    }
}
