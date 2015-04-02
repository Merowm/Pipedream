using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    //if the player is still alive
    public bool alive = true;
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

    //gameover screen
    public GameObject gameOverGUI;

    //particles
    //public GameObject partSysDead;

    void Awake(){
        healthGUI = GameObject.Find("healthGUI");
        gameOverGUI = GameObject.Find("gameOverGUI");
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











