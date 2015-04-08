using UnityEngine;
using System.Collections;

public class RGBColors : MonoBehaviour
{
    public Color32 color;
    public Color32 R;
    public Color32 G;
    public Color32 B;

    private UnityEngine.UI.Slider sliderR;
    private UnityEngine.UI.Slider sliderG;
    private UnityEngine.UI.Slider sliderB;

	void Awake ()
    {
        R = new Color(0,0,0);
        G = new Color(0,0,0);
        B = new Color(0,0,0);

        sliderR = transform.FindChild("Sliders").FindChild("R").GetComponent<UnityEngine.UI.Slider>();
        sliderG = transform.FindChild("Sliders").FindChild("G").GetComponent<UnityEngine.UI.Slider>();
        sliderB = transform.FindChild("Sliders").FindChild("B").GetComponent<UnityEngine.UI.Slider>();
	}

	void Update ()
    {
        R.r = (byte)sliderR.value;
        G.g = (byte)sliderG.value;
        B.b = (byte)sliderB.value;

        color = new Color((byte)sliderR.value,
                          (byte)sliderG.value,
                          (byte)sliderB.value);
	}
}
