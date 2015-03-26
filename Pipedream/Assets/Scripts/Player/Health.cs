using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    
    //maximum number of shield the player starts out with
    public int maxShield = 1;
    //current number of shield left that the player has
    public int currentShield = 1;

    //for shield regen
    public float shieldRegenTimer = 0;
    public float shieldRegenTime = 5;

    //maximum number of hull the player starts out with
    public int maxHull = 3;
    //current number of hull left that the player has
    public int currentHull = 3;

    //parent containing all the heath GUI
    public GameObject healthGUI;

    void Awake(){
        currentShield = maxShield;
        currentHull = maxHull;

        UpdateHealthGUI();
    }

    void Update(){
        UpdateShieldRegen();
    }

    //to damage ship
    public void Damage(){
        //if no shield left
        if (currentShield <= 0)
        {
            //damage hull
            --currentHull;
            Debug.Log("Received damage to hull");
        }
        //else
        else
        {
            //damage shield
            --currentShield;
            Debug.Log("Received damage to shields");
        }
        //reset shield regen timer
        shieldRegenTimer = 0;

        UpdateHealthGUI();
    }

    //to repair hull
    public void Repair(){
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
        for (int i = 0; i < healthGUI.transform.childCount; ++i)
        {
            healthGUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        //set correct GUI according to hull left and shields
        healthGUI.transform.FindChild("health_" + currentHull).gameObject.SetActive(true);
        if (currentShield > 0)
        {
            healthGUI.transform.FindChild("shield").gameObject.SetActive(true);
        }
    }

}











