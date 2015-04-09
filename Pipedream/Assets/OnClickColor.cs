using UnityEngine;
using System.Collections.Generic;

public class OnClickColor : MonoBehaviour
{
    public int buttonID;
    public Vector3 subPosition = new Vector3(0.0f, 273.5f, 0.0f);
    public List<GameObject> buttons;

    private enum STATES { Main, Sub };
    private STATES currentState;
    private Vector3 originalPosition;
    private GameObject sliders;

	void Awake ()
    {
        foreach (Transform child in transform.parent.parent)
        {
            buttons.Add(child.gameObject);
        }

        buttonID = GetID(buttons);
        currentState = STATES.Main;
        originalPosition = transform.parent.localPosition;
        sliders = transform.parent.FindChild("RGBSliders").gameObject;
	}

    public void ButtonClicked()
    {
        if (currentState == STATES.Main)
        {
            transform.parent.localPosition = subPosition;
            DisableOtherButtons();
            EnableSliders();
            currentState = STATES.Sub;
        }
        else
        {
            transform.parent.localPosition = originalPosition;
            EnableOtherButtons();
            DisableSliders();
            currentState = STATES.Main;
        }
    }

    void DisableOtherButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttonID != buttons[i].transform.GetComponentInChildren<OnClickColor>().buttonID)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    void EnableOtherButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    void EnableSliders()
    {
        sliders.SetActive(true);
    }

    void DisableSliders()
    {
        sliders.SetActive(false);
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
