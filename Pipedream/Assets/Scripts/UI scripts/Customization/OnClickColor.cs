using UnityEngine;
using System.Collections.Generic;

public class OnClickColor : MonoBehaviour
{
    public int buttonID;
    public Vector3 subPosition = new Vector3(0.0f, 270.0f, 0.0f);
    public List<GameObject> buttons = new List<GameObject>();

    private Vector3 originalPosition;
    private GameObject slidersParent;

	void Awake ()
    {
        foreach (Transform child in transform.parent.parent)
        {
            buttons.Add(child.gameObject);
        }

        buttonID = GetID(buttons);
        originalPosition = transform.parent.localPosition;
        slidersParent = transform.parent.FindChild("RGBSliders").gameObject;
	}

    public void ButtonClicked()
    {
        transform.parent.localPosition = subPosition;
        DisableButtons();
        EnableSliders();
        UpdateSliders();
        BackButton.editingColors = true;
    }

    public void Back()
    {
        transform.parent.localPosition = originalPosition;
        DisableSliders();
        BackButton.editingColors = false;
    }

    void UpdateSliders()
    {
        foreach (Transform child in slidersParent.transform)
        {
            if (child.GetComponent<RGBColors>() != null)
            {
                child.GetComponent<RGBColors>().SetSliders("current");
            }
        }
    }

    void DisableButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttonID != buttons[i].transform.GetComponentInChildren<OnClickColor>().buttonID)
            {
                buttons[i].gameObject.SetActive(false);
            }
            else
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }

    }

    void EnableSliders()
    {
        slidersParent.SetActive(true);
    }

    void DisableSliders()
    {
        slidersParent.SetActive(false);
    }

    int GetID (List<GameObject> list)
    {
        string name = transform.parent.name;
        
        for (int i = 0; i < list.Count; i++)
        {
            if (name == list[i].transform.name)
            {
                return i + 1;
            }
        }
        return 0;
    }
}
