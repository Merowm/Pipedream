using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    private Transform player;
    private Inventory inventory;
    private Health health;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponentInChildren<Inventory>();
        health = player.GetComponentInChildren<Health>();
	}

    public void Collect()
    {
        if (transform.name == "Repair")
        {
            inventory.items[0] = true;
            //Activates immediately
            inventory.UseItem(0);
        }
        else if (transform.name == "Invulnerability")
        {
            inventory.items[1] = true;
            //Activates when a collision happens in script "PlayerCollisions"
            health.partSysShield.maxParticles = (int)(health.originalEmissionRate * 0.8f);
            health.partSysShield.emissionRate = health.partSysShield.maxParticles / 2;
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
