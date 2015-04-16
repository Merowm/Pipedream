using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RGBColors : MonoBehaviour
{
    public Color32 color;
    public List<Color32> RGB;

    private UnityEngine.UI.Image image;
    private UnityEngine.UI.Slider sliderR;
    private UnityEngine.UI.Slider sliderG;
    private UnityEngine.UI.Slider sliderB;
    private Color32 R = new Color32(0,0,0,0);
    private Color32 G = new Color32(0,0,0,0);
    private Color32 B = new Color32(0,0,0,0);

	void Awake ()
    {
        image = transform.FindChild("Color").GetChild(0).GetComponent<UnityEngine.UI.Image>();
        sliderR = transform.FindChild("Sliders").FindChild("R").GetComponent<UnityEngine.UI.Slider>();
        sliderG = transform.FindChild("Sliders").FindChild("G").GetComponent<UnityEngine.UI.Slider>();
        sliderB = transform.FindChild("Sliders").FindChild("B").GetComponent<UnityEngine.UI.Slider>();
	}

	void Update ()
    {
        int rValue = (int)sliderR.value;
        int gValue = (int)sliderG.value;
        int bValue = (int)sliderB.value;

        R = new Color32((byte)rValue,0,0,255);
        G = new Color32(0,(byte)gValue,0,255);
        B = new Color32(0,0,(byte)bValue,255);

        color = new Color32(R.r, G.g, B.b, 255);

        if (image != null)
        {
            image.color = color;
        }
        else
        {
            Debug.Log("Image not found!");
        }

        RGB.Clear();
        RGB.Add(R);
        RGB.Add(G);
        RGB.Add(B);
	}

    void SetSliders (int r, int g, int b)
    {
        sliderR.value = r;
        sliderG.value = g;
        sliderB.value = b;
    }
}
