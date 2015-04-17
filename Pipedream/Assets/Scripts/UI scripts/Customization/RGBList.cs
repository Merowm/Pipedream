using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RGBList : MonoBehaviour
{
    public List<UnityEngine.UI.Slider> RGB;

    public List<int> sliderValues;

	void Awake ()
    {
        RGB = transform.GetComponentsInChildren<UnityEngine.UI.Slider>().ToList();
        for (int i = 0; i < RGB.Count; i++)
        {
            sliderValues.Add((int)RGB[i].value);
        }
	}

    void OnEnable ()
    {
        //transform.parent.GetComponent<RGBSliders>().rgb.Add(RGB);
    }
}
