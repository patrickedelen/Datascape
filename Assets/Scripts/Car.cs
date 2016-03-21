using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public float maxTorque = 155f;
    public float steerForce = 2f;
    public float maxSpeed = 600f;

    public float speed;

    public Transform centerOfMass;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];

    private Rigidbody m_rigidBody;
    private LevelController LC;
    private GameObject pauseMenu;
    private bool pauseToggle;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
        LC = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        UpdateMeshesPositions();
        speed = m_rigidBody.velocity.sqrMagnitude;

        //if paused
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseToggle)
        {
            Debug.Log("pause");
            pauseToggle = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseToggle)
        {
            pauseToggle = false;

            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    void FixedUpdate()
    {


        float steer = Input.GetAxis("Horizontal");
        float accelerate = Input.GetAxis("Vertical");

        float fAngle = steer * 30f;
        wheelColliders[0].steerAngle = fAngle;
        wheelColliders[1].steerAngle = fAngle;

        if(m_rigidBody.velocity.sqrMagnitude > 400f)
        {
            LC.TooFast = true;
        } else
        {
            LC.TooFast = false;
        }


        if (m_rigidBody.velocity.sqrMagnitude < (maxSpeed / 5))
        {
            foreach (WheelCollider wc in wheelColliders)
            {
                wc.motorTorque = accelerate * maxTorque * 2;
            }
        } else if (m_rigidBody.velocity.sqrMagnitude < maxSpeed)
        {
            foreach (WheelCollider wc in wheelColliders)
            {
                wc.motorTorque = accelerate * maxTorque;
            }
        } else
        {
            foreach (WheelCollider wc in wheelColliders)
            {
                wc.motorTorque = 0;
            }

        }

    }

    void UpdateMeshesPositions()
    {
        for(int i = 0; i < wheelColliders.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;

            wheelColliders[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }

}
