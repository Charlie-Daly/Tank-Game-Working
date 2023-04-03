using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    //The amount of health each tank starts with
    public float m_StartingHealth = 100f;

    //a prefab that will be instantiated in awake, then used whenever
    //the tank dies
    public GameObject m_ExplosionPrefab;

    public GameObject m_TankPrefab;

    private float m_currentHealth;
    private bool m_Dead;
    //The particle system that will play when the tank is destroyed
    private ParticleSystem m_ExplosionParticles;

    EnemyTankMovement enemyTankMovement;

    public Transform thePosition;

    // Start is called before the first frame update
    private void Awake()
    {
        //Instaniate the explosion prefab and get a reference to 
        //the particle system on it
        m_ExplosionParticles =
            Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        //Disable the prefab so it can be activated when it's required
        m_ExplosionParticles.gameObject.SetActive(false);


        //thePosition = GameObject.FindGameObjectWithTag("EnemyReturnBase");
    }

    private void OnEnable()
    {
        //when the tank is enabled, reset the tank's health and whether
        //or not it's dead
        m_currentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    private void SetHealthUI()
    {

    }

    public void TakeDamage(float amount)
    {
        //Reduce current health by the amount of damage done
        m_currentHealth -= amount;

        //Change the UI elements appropriately
        SetHealthUI();

        //if the current health is at or below zero and it has not yet
        //been registered, call OnDeath
        if (m_currentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        //set the flag so that this function is only called once
        m_Dead = true;

        //Move the instantiated explosion prefab to the tank's position
        //and turn it on
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        //Play the particle system of the tank exploding
        m_ExplosionParticles.Play();

        //Instantiate(m_TankPrefab, thePosition.position, thePosition.rotation);

        //turn the tank off
        gameObject.SetActive(false);

        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
