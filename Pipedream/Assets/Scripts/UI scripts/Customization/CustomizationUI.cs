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

    public void DeactivateUI ()
    {
        if (transform.FindChild("pauseScreen").gameObject.activeSelf)
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

    public void CollapseUI ()
    {
        if (transform.FindChild("CustomizationSheet").gameObject.activeSelf)
        {
            transform.FindChild("CustomizationSheet").gameObject.SetActive(false);
        }
        else transform.FindChild("CustomizationSheet").gameObject.SetActive(true);

        transform.FindChild("CollapseUI").transform.Rotate(new Vector3(0,0,180));
    }
}
