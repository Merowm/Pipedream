using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RGBColors : MonoBehaviour
{
    public Color32 color;
    public int colorIndex;
    public List<Color32> RGB;
    public List<int> RGBValues;

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

        UpdateRGB();
	}

    /// <summary>
    /// Available colorTypes: "current", "inMemory", "default"
    /// </summary>
    public void SetSliders (string colorType)
    {
        ChangeColors changeColors = (ChangeColors)FindObjectOfType(typeof(ChangeColors));
        Color32 color = new Color(255,255,255,255);

        if (colorType == "current")
        {
            color = changeColors.images[colorIndex].color;
        }
        else if (colorType == "inMemory")
        {
            color = changeColors.colorsInMemory[colorIndex];
        }
        else if (colorType == "default")
        {
            color = changeColors.colorsDefault[colorIndex];
        }
        else Debug.Log("Wrong Color Type in " + transform.gameObject + " -> RGBColors -> SetSliders");

        sliderR.value = color.r;
        sliderG.value = color.g;
        sliderB.value = color.b;
    }

    void UpdateRGB ()
    {
        RGB.Clear();
        RGB.Add(R);
        RGB.Add(G);
        RGB.Add(B);
    }
}
