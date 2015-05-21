using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//[ExecuteInEditMode]
public class ChangeColors : MonoBehaviour
{
    public List<UnityEngine.UI.Image> images;
    public List<Color32> colorsCurrent;
    public List<Color32> colorsInMemory;
    public List<Color32> colorsDefault;
    public Material hyperTunnelMaterial;
    public Material shipHullMaterial;
    public Material shipMarkingsMaterial;
    public Material shipThrusterMaterial;
    public Material wireframeMaterial;
    public Material orbMaterial;

    private ChangeLighting lighting;
    private List<ParticleSystem> particles;
    private ParticleSystem shield;
    private ParticleSystem thruster;
    private List<GameObject> buttons;

    private Statistics statistics;

	void Awake ()
    {
        if (GameObject.FindWithTag("statistics") != null)
        {
            statistics = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
            LoadColorsInMemory();
        }

        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = colorsInMemory[i];
        }
        for (int i = 0; i < images.Count; i++)
        {
            colorsCurrent.Add(images[i].color);
        }
        //UpdateLists();
        //hyperTunnelMaterial.color = new Color32(100,100,100,255);
        lighting = (ChangeLighting)FindObjectOfType(typeof(ChangeLighting));
        particles = GameObject.FindGameObjectWithTag("effects").GetComponentsInChildren<ParticleSystem>().ToList();
        shield = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Ship").
            FindChild("Shield Particle System").GetComponent<ParticleSystem>();
        thruster = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Ship").
            FindChild("ThrusterParticles").GetComponent<ParticleSystem>();
        
        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.parent.parent.GetComponent<RGBColors>().colorIndex = i;
        }
	}

	void Update ()
    {
        for (int i = 0; i < images.Count; i++)
        {
            colorsCurrent[i] = images[i].color;
        }

        for (int i = 0; i < images.Count; i++)
        {
            if (images[i].transform.parent.parent.name == "RGBSliders_HyperTunnel")
            {
                hyperTunnelMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_RingGates")
            {
                lighting.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Outside")
            {
                for (int n = 0; n < particles.Count; n++)
                {
                    particles[n].startColor = colorsCurrent[i];
                }
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Hull")
            {
                shipHullMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Markings")
            {
                shipMarkingsMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Shields")
            {
                shield.startColor = new Color32(colorsCurrent[i].r,colorsCurrent[i].g,colorsCurrent[i].b,170);
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Thruster")
            {
                shipThrusterMaterial.color = colorsCurrent[i];
                thruster.startColor = new Color32(colorsCurrent[i].r,colorsCurrent[i].g,colorsCurrent[i].b,255);
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Wireframes")
            {
                wireframeMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Orbs")
            {
                orbMaterial.color = colorsCurrent[i];
            }
        }
	}

    public void UpdateLists ()
    {
        if (colorsDefault.Count != images.Count)
        {
            images.Clear();
            colorsCurrent.Clear();

            GameObject[] colorPalettes = GameObject.FindGameObjectsWithTag("ColorPalette");
                
            for (int i = 0; i < colorPalettes.Length; i++)
            {
                //images.Add(colorPalettes[i].GetComponent<UnityEngine.UI.Image>());
            }
            for (int i = 0; i < images.Count; i++)
            {
                colorsCurrent.Add(images[i].transform.GetComponent<UnityEngine.UI.Image>().color);
            }
        }
    }

    public void LoadColorsInMemory ()
    {
        List<Color32> col = statistics.GetCustoms().ToList();

        colorsInMemory.Clear();

        for (int i = 0; i < col.Count; i++)
        {
            colorsInMemory.Add(col[i]);
        }
    }

    public void SaveColorsToMemory ()
    {
        colorsInMemory.Clear();

        for (int i = 0; i < images.Count; i++)
        {
            colorsInMemory.Add(colorsCurrent[i]);
        }

        Color32[] memorized = colorsInMemory.ToArray();
        statistics.SaveCustoms(memorized);
    }
}
