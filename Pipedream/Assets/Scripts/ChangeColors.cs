using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ChangeColors : MonoBehaviour
{
    public List<CanvasRenderer> canvases;
    public List<Color> colors;
    public List<Color> lastColors; //Colors last frame

    private ChangeLighting lighting;

	void Start ()
    {
        CanvasRenderer[] canvasesArray = transform.GetComponentsInChildren<CanvasRenderer>();

        if (canvases.Count < canvasesArray.Length || lastColors.Count < canvasesArray.Length)
        {
            canvases.Clear();
            colors.Clear();
            lastColors.Clear();

            for (int i = 0; i < canvasesArray.Length; i++)
            {
                canvases.Add(canvasesArray[i]);
                colors.Add(canvases[i].GetColor());
            }

            for (int i = 0; i < colors.Count; i++)
            {
                lastColors.Add(colors[i]);
            }
        }

        lighting = (ChangeLighting)FindObjectOfType(typeof(ChangeLighting));
	}

	void Update ()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i] != lastColors[i])
            {
                //Update lastColor
                lastColors[i] = colors[i];
                //Changing the color of the CanvasRenderer
                //canvases[i].SetColor(colors[i]);
                UnityEngine.UI.Image image = canvases[i].GetComponent<UnityEngine.UI.Image>(); 
                image.color = colors[i];
                //Changing the normalColor of the Button
                UnityEngine.UI.Button button = canvases[i].GetComponent<UnityEngine.UI.Button>(); 
                UnityEngine.UI.ColorBlock colorBlock = button.colors;
                colorBlock.normalColor = colors[i];
                button.colors = colorBlock;

                lighting.color = colors[0];
            }
        }
	}
}
