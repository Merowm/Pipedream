using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class ChangeColors : MonoBehaviour
{
    public List<UnityEngine.UI.Image> images;
    public List<Color> colors;
    public Material wireframeMaterial;

    private ChangeLighting lighting;
    private List<ParticleSystem> particles;

	void Awake ()
    {
        UpdateLists();
        /*GameObject[] colorPalettes = GameObject.FindGameObjectsWithTag("ColorPalette");

        if (images.Count != colorPalettes.Length)
        {
            images.Clear();
            colors.Clear();

            for (int i = 0; i < colorPalettes.Length; i++)
            {
                images.Add(colorPalettes[i].GetComponent<UnityEngine.UI.Image>());
            }
            for (int i = 0; i < images.Count; i++)
            {
                colors.Add(images[i].transform.GetComponent<UnityEngine.UI.Image>().color);
            }
        }*/

        lighting = (ChangeLighting)FindObjectOfType(typeof(ChangeLighting));
        particles = GameObject.FindGameObjectWithTag("effects").GetComponentsInChildren<ParticleSystem>().ToList();
	}

	void Update ()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            colors[i] = images[i].color;
        }

        for (int i = 0; i < images.Count; i++)
        {
            if (images[i].name == "RingGateColor")
            {
                lighting.color = colors[i];
            }
            else if (images[i].name == "HyperTunnelColor")
            {
                for (int n = 0; n < particles.Count; n++)
                {
                    //particles[n] = colors[i];
                }
            }
            else if (images[i].name == "WireframeColor")
            {
                wireframeMaterial.color = colors[i];
            }
        }
	}

    public void UpdateLists ()
    {
        GameObject[] colorPalettes = GameObject.FindGameObjectsWithTag("ColorPalette");
        
        if (images.Count != colorPalettes.Length)
        {
            images.Clear();
            colors.Clear();
            
            for (int i = 0; i < colorPalettes.Length; i++)
            {
                images.Add(colorPalettes[i].GetComponent<UnityEngine.UI.Image>());
            }
            for (int i = 0; i < images.Count; i++)
            {
                colors.Add(images[i].transform.GetComponent<UnityEngine.UI.Image>().color);
            }
        }
    }
}
