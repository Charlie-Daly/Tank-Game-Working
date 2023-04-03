using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    //the tank will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 8f;
    //the tank's turret object
    public Transform m_Turret;

    //a reference to the player - this will be set when the enemy is loaded
    private GameObject m_Player;
    //A reference to the nav mesh agent component
    private NavMeshAgent m_NavAgent;
    //A reference to the rigidbody componet
    private Rigidbody m_Rigidbody;

    //Were the Enemy will return to
    private GameObject m_ReturnBase;

    //Will be set to true when this tank should follow the player
    private bool m_Follow;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;

        m_ReturnBase = GameObject.FindGameObjectWithTag("EnemyReturnBase");
    }

    private void OnEnable()
    {
        //when the tank is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        //when the tank is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Follow == false)
        {
            float BaseDistance = (m_ReturnBase.transform.position - transform.position).magnitude;
            if (BaseDistance > m_CloseDistance)
            {
                m_NavAgent.SetDestination(m_ReturnBase.transform.position);
                m_NavAgent.isStopped = false;
            }
            else
            {
                m_NavAgent.isStopped = true;
            }

            if (m_Turret != null)
            {
                m_Turret.LookAt(m_ReturnBase.transform);
            }
            return;
        }

        //get distance form player to enemy tank
        float distance = (m_Player.transform.position - transform.position).magnitude;
        //if distance is less than stop distance, then stop moving
        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_Player.transform.position);
            m_NavAgent.isStopped = false;
        }
        else
        {
            m_NavAgent.isStopped = true;
        }

        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Player.transform);
        }
    }
}
