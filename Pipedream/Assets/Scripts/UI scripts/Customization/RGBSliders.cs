using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RGBSliders : MonoBehaviour
{
    public List<GameObject> sliders;

    public List<UnityEngine.UI.Image> images;

	void Awake ()
    {
        foreach(Transform child in transform)
        {
            sliders.Add(child.gameObject);
        }
        /*for (int i = 0; i < transform.parent.parent.GetComponent<ChangeColors>().images.Count; i++)
        {
            images = transform.parent.parent.GetComponent<ChangeColors>().images;
        }*/

        for (int i = 0; i < sliders.Count; i++)
        {
            for (int n = 0; n < sliders.Count; n++)
            {
                //sliders[i].GetComponent<RGBColors>().SetSliders();
            }
        }
	}

	void Update ()
    {
	    
	}
}
