using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour
{
    private Transform player;
    private Inventory inventory;
    private GameObject repair;
    private GameObject invulnerability;
    private GameObject timeSlow;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponentInChildren<Inventory>();
        repair = transform.FindChild("Repair").gameObject;
        invulnerability = transform.FindChild("Invulnerability").gameObject;
        timeSlow = transform.FindChild("TimeSlow").gameObject;
	}

    void Update ()
    {
        if (inventory.items[0])
        {
            repair.GetComponent<UnityEngine.UI.Image>().enabled = false;
            repair.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            repair.GetComponent<UnityEngine.UI.Image>().enabled = true;
            repair.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (inventory.items[1])
        {
            invulnerability.GetComponent<UnityEngine.UI.Image>().enabled = false;
            invulnerability.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            invulnerability.GetComponent<UnityEngine.UI.Image>().enabled = true;
            invulnerability.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (inventory.items[2])
        {
            timeSlow.GetComponent<UnityEngine.UI.Image>().enabled = false;
            timeSlow.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            timeSlow.GetComponent<UnityEngine.UI.Image>().enabled = true;
            timeSlow.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

	public void Repair ()
    {
        inventory.UseItem(0);
	}

    public void Invulnerability ()
    {
        inventory.UseItem(1);
    }

    public void TimeSlow ()
    {
        inventory.UseItem(2);
    }
}
