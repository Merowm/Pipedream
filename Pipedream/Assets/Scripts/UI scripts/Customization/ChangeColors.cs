using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class ChangeColors : MonoBehaviour
{
    public List<UnityEngine.UI.Image> images;
    public List<Color> colors;
    public Material hyperTunnelMaterial;
    public Material wireframeMaterial;

    private ChangeLighting lighting;
    private List<ParticleSystem> particles;

	void Awake ()
    {
        UpdateLists();
        //hyperTunnelMaterial.color = new Color32(100,100,100,255);
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
            if (images[i].transform.parent.parent.name == "RGBSliders_RingGates")
            {
                lighting.color = colors[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_HyperTunnel")
            {
                hyperTunnelMaterial.color = colors[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Outside")
            {
                for (int n = 0; n < particles.Count; n++)
                {
                    particles[n].startColor = colors[i];
                }
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Wireframes")
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
