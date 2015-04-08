﻿using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ChangeColors : MonoBehaviour
{
    public List<CanvasRenderer> canvases;
    public List<Color> colors;
    public List<Color> lastColors; //Colors last frame

    private ChangeLighting lighting;

	void Awake ()
    {
        GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");

        if (canvases.Count != colorButtons.Length || lastColors.Count != colorButtons.Length)
        {
            canvases.Clear();
            colors.Clear();
            lastColors.Clear();

            for (int i = 0; i < colorButtons.Length; i++)
            {
                canvases.Add(colorButtons[i].GetComponent<CanvasRenderer>());
                colors.Add(canvases[i].transform.GetComponent<UnityEngine.UI.Image>().color);
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
