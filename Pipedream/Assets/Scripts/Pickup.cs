using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    private Transform player;
    private Inventory inventory;
    private Health health;
    private ParticleSystem shieldOn;
    private VolControl sound;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponentInChildren<Inventory>();
        health = player.GetComponentInChildren<Health>();
        shieldOn = GameObject.Find("shieldEffect").GetComponent<ParticleSystem>();
        sound = GameObject.FindGameObjectWithTag("statistics").GetComponent<VolControl>();
	}

    public void Collect()
    {
        sound.PlayCollectSound(this.transform.position);
        if (transform.name == "Repair")
        {
            inventory.items[0] = true;
            //Activates immediately
            inventory.UseItem(0);
        }
        else if (transform.name == "Invulnerability")
        {
            if (!inventory.items[1])
            {
                if (!health.invulnerable)
                {
                    inventory.items[1] = true;
                    //Play GUI effect
                    shieldOn.Play();
                    //Set shield to overdrive
                    health.shieldInOverdrive = true;
                }
                else
                {
                    //Play GUI effect
                    shieldOn.Play();
                    health.invulnerabilityTimer = 0;
                }
            }
        }
        else if (transform.name == "TimeSlow")
        {
            inventory.items[2] = true;
            //Activates immediately
            inventory.UseItem(2);
        }

        Destroy(transform.parent.gameObject);
    }
}
