using UnityEngine;
using System.Collections.Generic;

public class RestoreDefaults : MonoBehaviour
{
    public List<UnityEngine.UI.Image> images;
    public List<Color32> colorDefaults;

	void Awake ()
    {
        if (transform.parent.name == "CustomizationUI")
        {
            images = transform.parent.transform.GetComponentInChildren<ChangeColors>().images;
            colorDefaults = transform.parent.transform.GetComponentInChildren<ChangeColors>().colorDefaults;
        }
	}

	public void Restore ()
    {
        if (transform.parent.name == "CustomizationUI")
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].color = colorDefaults[i];
            }
            //transform.parent.transform.GetComponentInChildren<ChangeColors>().images.Clear();
            //transform.parent.transform.GetComponentInChildren<ChangeColors>().images = images;
        }
	}
}
