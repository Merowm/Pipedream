using UnityEngine;
using System.Collections.Generic;

public class CustomizationUI : MonoBehaviour
{
    public List<GameObject> objectsUI;
    public GameObject pauseScreen;

	void Awake ()
    {
        pauseScreen = transform.FindChild("pauseScreen").gameObject;

        foreach (Transform child in transform)
        {
            if (child.gameObject != pauseScreen)
            {
                objectsUI.Add(child.gameObject);
            }
        }
	}

	void Update ()
    {
	    if (pauseScreen.activeSelf)
        {
            for (int i = 0; i < objectsUI.Count; i++)
            {
                objectsUI[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < objectsUI.Count; i++)
            {
                objectsUI[i].SetActive(true);
            }
        }
	}
}
