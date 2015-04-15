﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    //if the player is still alive
    private bool alive = true;
    //maximum number of shield the player starts out with
    private int maxShield = 1;
    //current number of shield left that the player has
    private int currentShield = 1;

    //for shield regen
    private float shieldRegenTimer = 0;
    private float shieldRegenTime = 5;

    //maximum number of hull the player starts out with
    private int maxHull = 3;
    //current number of hull the player has left
    private int currentHull = 3;

    //health GUIs
    private GameObject healthGUI0;
    private GameObject healthGUI1;
    private GameObject healthGUI2;
    private GameObject healthGUI3;
    private GameObject healthGUIShield;

    //gameover screen
    private GameObject gameOverGUI;

    //shield particle system
    private ParticleSystem partSysShield;

    //particles
    //public GameObject partSysDead;

    void Awake(){
        GameObject healthParent = GameObject.Find("healthGUI");
        healthGUI0 = healthParent.transform.FindChild("health_0").gameObject;
        healthGUI1 = healthParent.transform.FindChild("health_1").gameObject;
        healthGUI2 = healthParent.transform.FindChild("health_2").gameObject;
        healthGUI3 = healthParent.transform.FindChild("health_3").gameObject;
        healthGUIShield = healthParent.transform.FindChild("shield").gameObject;
        gameOverGUI = GameObject.Find("gameOverGUI");
        partSysShield = GameObject.Find("Shield Particle System").GetComponent <ParticleSystem>();
        Reset();
    }

    void Update(){
        UpdateShieldRegen();

        if (!alive)
        {
           // Controls.controlsActivated = false;
        }
    }

    //to damage ship
    public void Damage(){
        if (!alive)
        {
            return;
        }
        //if no shield left
        if (currentShield <= 0)
        {
            //damage hull
            --currentHull;
            Debug.Log("Received damage to hull");
            if (currentHull <= 0){
                Debug.Log("Game Over");
                gameOverGUI.SetActive(true);
                alive = false;
                Time.timeScale = 0.0f;
                //partSysDead.SetActive(true);
            }
        }
        //else
        else
        {
            //damage shield
            --currentShield;
            partSysShield.emissionRate = 0;
            Debug.Log("Received damage to shields");
        }
        //reset shield regen timer
        shieldRegenTimer = 0;

        UpdateHealthGUI();
    }

    //to repair hull
    public void Repair(){
        if (!alive)
        {
            return;
        }
        //if hull not full
        if (currentHull < maxHull)
        {
            //add hull
            ++currentHull;
            Debug.Log("Hull repaired");
        }

        UpdateHealthGUI();
    }

    //updats shield regen
    private void UpdateShieldRegen(){
        if (!alive)
        {
            return;
        }
        //if shields not full
        if (currentShield < maxShield)
        {
            //update timer
            shieldRegenTimer += Time.deltaTime;
            //if timer is up
            if (shieldRegenTimer >= shieldRegenTime){
                //reset timer
                shieldRegenTimer -= shieldRegenTime;
                //add shield
                ++currentShield;
                //if shields full
                if (currentShield >= maxShield){
                    //set timer to 0
                    shieldRegenTimer = 0;
                }
                Debug.Log("Shield regenerated");
                partSysShield.emissionRate = 15;
                UpdateHealthGUI();
            }
        }
    }

    private void UpdateHealthGUI(){
        if (currentHull < 0)
        {
            return;
        }
        //disable all health GUI
        healthGUI0.SetActive(false);
        healthGUI1.SetActive(false);
        healthGUI2.SetActive(false);
        healthGUI3.SetActive(false);
        //set correct GUI according to hull left and shields
        switch (currentHull)
        {
            case 0:
                healthGUI0.SetActive(true);
                break;
            case 1:
                healthGUI1.SetActive(true);
                break;
            case 2:
                healthGUI2.SetActive(true);
                break;
            case 3:
                healthGUI3.SetActive(true);
                break;
        }
        if (currentShield > 0)
        {
            healthGUIShield.SetActive(true);
        }
        else
        {
            healthGUIShield.SetActive(false);
        }
    }

    public void Reset(){
        gameOverGUI.SetActive(false);
        alive = true;
        currentShield = maxShield;
        currentHull = maxHull;
        shieldRegenTimer = 0;
        UpdateHealthGUI();
        Time.timeScale = 1;
        //partSysDead.SetActive(false);
    }

}











