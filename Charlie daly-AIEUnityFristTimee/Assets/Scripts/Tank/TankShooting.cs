using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{

    //Prefab of the shell
    public Rigidbody m_Shell;
    //A child of the tank where the shells are spawned
    public Transform m_FireTransform;
    //The force given to the shell when firing
    public float m_LaunchForce = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        //Create an instance of the shell and store a reference to its rigidbody
        Rigidbody shellInstanse = Instantiate(m_Shell,
                                              m_FireTransform.position,
                                              m_FireTransform.rotation) as Rigidbody;

        //set the shell's velocity to the launch force in the fire
        //position's forward direction
        shellInstanse.velocity = m_LaunchForce * m_FireTransform.forward;
    }
}
