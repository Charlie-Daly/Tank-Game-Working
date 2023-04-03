using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float m_DampTime = 0.2f;

    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    private float camHeightSetup = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {

        //scroll increas to either -0.1 or 0.1 so have it Plus to Camera.main.orthographicSize rather than equal 
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        camHeightSetup += scroll;

        float cameraHeight = Mathf.Clamp(camHeightSetup, 1, 50);

        Camera.main.orthographicSize = camHeightSetup;

        Debug.Log(scroll);
        /*
               if (Camera.main.orthographicSize <= 1)
               {
                  Camera.main.orthographicSize = 1;
               }
               else
               {
                    Camera.main.orthographicSize = cameraHeight;
               }
            }
        */
    }
     void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position,
                  m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}
