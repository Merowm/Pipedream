using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackButton : MonoBehaviour
{
    public static bool editingColors = false;
    public List<GameObject> buttons = new List<GameObject>();

    private GameObject exitWarning;
    private GameObject overrideWarning;

    void Awake ()
    {
        foreach (Transform child in transform.parent.FindChild("Colors").transform)
        {
            buttons.Add(child.gameObject);
        }
        exitWarning = GameObject.FindGameObjectWithTag("ExitCheck").gameObject;
        exitWarning.SetActive(false);
        overrideWarning = GameObject.FindGameObjectWithTag("OverrideCheck").gameObject;
        overrideWarning.SetActive(false);
    }

    public void BackClicked()
    {
        if (!editingColors)
        {
            exitWarning.SetActive(true);
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons [i].gameObject.SetActive(true);
                buttons [i].transform.GetChild(1).gameObject.SetActive(true);
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons [i].gameObject.activeSelf)
                {
                    buttons [i].transform.GetComponentInChildren<OnClickColor>().Back();
                }
            }
        }
    }
}
