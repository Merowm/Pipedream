using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    private Transform player;
    private Inventory inventory;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponentInChildren<Inventory>();
	}

    public void Collect()
    {
        if (transform.name == "Repair")
        {
            inventory.items[0] = true;
            inventory.UseItem(0);
        }
        else if (transform.name == "Invulnerability")
        {
            inventory.items[1] = true;
        }
        else if (transform.name == "TimeSlow")
        {
            inventory.items[2] = true;
        }

        Destroy(transform.parent.gameObject);
    }
}
