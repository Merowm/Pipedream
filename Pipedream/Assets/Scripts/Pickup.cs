using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    private Transform player;
    private Inventory inventory;
    private Health health;
    private ParticleSystem shieldOn;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponentInChildren<Inventory>();
        health = player.GetComponentInChildren<Health>();
        shieldOn = GameObject.Find("shieldEffect").GetComponent<ParticleSystem>();
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
            if (!inventory.items[1])
            {
                if (!health.invulnerable)
                {
                    inventory.items[1] = true;
                    //Play GUI effect
                    shieldOn.Play();
                    //Activates when a collision happens in script "PlayerCollisions"
                    Debug.Log((int)(health.originalEmissionRate * 0.8f) + " MAX");
                    Debug.Log((health.partSysShield.maxParticles / 2) + " EMISSION");
                    health.partSysShield.maxParticles = 6;
                    health.partSysShield.emissionRate = health.originalEmissionRate * 3.0f;
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
