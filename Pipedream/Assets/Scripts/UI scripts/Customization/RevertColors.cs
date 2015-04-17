using UnityEngine;
using System.Collections.Generic;

public class RevertColors : MonoBehaviour
{
    public List<UnityEngine.UI.Image> images;
    public List<Color32> colorsDefault;
    public List<Color32> colorsInMemory;
    public List<GameObject> buttonSlots;

	void Awake ()
    {
        ChangeColors changeColors = transform.GetComponent<ChangeColors>();
        images = changeColors.images;
        colorsDefault = changeColors.colorsDefault;
        colorsInMemory = changeColors.colorsInMemory;

        foreach (Transform child in transform)
        {
            buttonSlots.Add(child.gameObject);
        }
	}

	public void RestoreDefaults ()
    {
        List<int> amountIndex = activeAmountIndex();

        if (amountIndex[0] == 0)
        {
            Debug.Log("No buttons active");
        }
        else if (amountIndex[0] == buttonSlots.Count)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].color = colorsDefault[i];
            }
        }
        else
        {
            foreach (Transform child in buttonSlots[amountIndex[1]].transform.FindChild("RGBSliders").transform)
            {
                child.GetComponent<RGBColors>().SetSliders("default");
            }
        }
	}

    public void RevertChanges ()
    {
        List<int> amountIndex = activeAmountIndex();
        
        if (amountIndex[0] == 0)
        {
            Debug.Log("No buttons active");
        }
        else if (amountIndex[0] == buttonSlots.Count)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].color = colorsInMemory[i];
            }
        }
        else
        {
            foreach (Transform child in buttonSlots[amountIndex[1]].transform.FindChild("RGBSliders").transform)
            {
                child.GetComponent<RGBColors>().SetSliders("inMemory");
            }
        }
    }

    List<int> activeAmountIndex ()
    {
        int activeButtonsAmount = 0;
        int index = 0;

        for (int i = 0; i < buttonSlots.Count; i++)
        {
            if (buttonSlots[i].activeSelf)
            {
                activeButtonsAmount += 1;
                index = i;
            }
        }

        List<int> pair = new List<int>();
        pair.Add(activeButtonsAmount);
        pair.Add(index);

        return pair;
    }
}
