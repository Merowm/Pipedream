using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//[ExecuteInEditMode]
public class ChangeColors : MonoBehaviour
{
    public List<UnityEngine.UI.Image> images;
    public List<Color32> colors;
    //public List<string> namesInMemory;
    public List<Color32> colorsInMemory;
    public List<Color32> colorDefaults;
    public Material hyperTunnelMaterial;
    public Material shipMaterial;
    public Material wireframeMaterial;

    private ChangeLighting lighting;
    private List<ParticleSystem> particles;
    private ParticleSystem shield;

	void Awake ()
    {
        LoadColorsInMemory();
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = colorsInMemory[i];
        }
        //UpdateLists();
        //hyperTunnelMaterial.color = new Color32(100,100,100,255);
        lighting = (ChangeLighting)FindObjectOfType(typeof(ChangeLighting));
        particles = GameObject.FindGameObjectWithTag("effects").GetComponentsInChildren<ParticleSystem>().ToList();
        shield = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Ship").
            FindChild("Shield Particle System").GetComponent<ParticleSystem>();
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
            else if (images[i].transform.parent.parent.name == "RGBSliders_Hull")
            {
                shipMaterial.color = colors[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Shields")
            {
                shield.startColor = new Color32(colors[i].r,colors[i].g,colors[i].b,170);
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Thruster")
            {
                
            }
        }
	}

    public void UpdateLists ()
    {
        if (colorDefaults.Count != images.Count)
        {
            images.Clear();
            colors.Clear();

            GameObject[] colorPalettes = GameObject.FindGameObjectsWithTag("ColorPalette");
                
            for (int i = 0; i < colorPalettes.Length; i++)
            {
                //images.Add(colorPalettes[i].GetComponent<UnityEngine.UI.Image>());
            }
            for (int i = 0; i < images.Count; i++)
            {
                colors.Add(images[i].transform.GetComponent<UnityEngine.UI.Image>().color);
            }
        }
    }

    public void LoadColorsInMemory ()
    {
        //colorsInMemory.Clear();

        /*colorsInMemory.Add(hyperTunnelMaterial.color);
        colorsInMemory.Add(lighting.color);
        colorsInMemory.Add(particles[0].startColor);
        colorsInMemory.Add(shipMaterial.color);
        colorsInMemory.Add(shield.startColor);
        colorsInMemory.Add(new Color32(255,255,255,255)); //TODO:colorsInMemory.Add(thruster.color);
        colorsInMemory.Add(wireframeMaterial.color);*/
    }

    public void SaveColorsToMemory ()
    {
        colorsInMemory.Clear();

        for (int i = 0; i < images.Count; i++)
        {
            colorsInMemory.Add(colors[i]);
        }
    }
}
