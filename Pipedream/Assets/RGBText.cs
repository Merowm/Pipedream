using UnityEngine;
using System.Collections;

public class RGBText : MonoBehaviour
{
    public float value = 255;

    private UnityEngine.UI.Slider slider;
    private UnityEngine.UI.Text text;

	void Awake ()
    {
        slider = transform.parent.GetComponent<UnityEngine.UI.Slider>();
        text = transform.GetComponent<UnityEngine.UI.Text>();
	}

	void Update ()
    {
        value = slider.value;
        text.text = value.ToString();
	}
}
