using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackButton : MonoBehaviour
{
    public static bool editingColors = false;
    public List<GameObject> buttons = new List<GameObject>();

    private GoToNext goTo;

    void Awake ()
    {
        foreach (Transform child in transform.parent.FindChild("Colors").transform)
        {
            buttons.Add(child.gameObject);
        }
        goTo = transform.GetComponent<GoToNext>();
    }

    public void BackClicked()
    {
        if (!editingColors)
        {
            goTo.GoToScene();
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
