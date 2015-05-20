using UnityEngine;
using System.Collections.Generic;

public class CustomizationUI : MonoBehaviour
{
    public List<GameObject> objectsUI;
    public GameObject pauseScreen;

    private GameObject customizationSheet;
    private GameObject collapseUI;
    private bool uiCollapsed = false;

	void Awake ()
    {
        pauseScreen = transform.FindChild("pauseScreen").gameObject;
        customizationSheet = transform.FindChild("CustomizationSheet").gameObject;
        collapseUI = customizationSheet.transform.FindChild("CollapseUI").gameObject;

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
        if (!uiCollapsed)
        {
            foreach(Transform child in customizationSheet.transform)
            {
                if (child.name != "CollapseUI")
                {
                    child.gameObject.SetActive(false);
                }
            }
            uiCollapsed = true;
        }
        else
        {
            foreach(Transform child in customizationSheet.transform)
            {
                if (child.name != "CollapseUI")
                {
                    child.gameObject.SetActive(true);
                }
            }
            uiCollapsed = false;
        }
        collapseUI.transform.Rotate(new Vector3(0,0,180));
    }
}
