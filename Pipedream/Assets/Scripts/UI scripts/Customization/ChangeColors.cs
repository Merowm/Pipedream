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
    public Material shipMaterial;
    public Material wireframeMaterial;

    private ChangeLighting lighting;
    private List<ParticleSystem> particles;
    private ParticleSystem shield;
    private List<GameObject> buttons;

	void Awake ()
    {
        LoadColorsInMemory();
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
        
        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.parent.parent.GetComponent<RGBColors>().colorIndex = i;
        }
	}

    void Start ()
    {
        /*foreach (Transform child in transform)
        {
            foreach (Transform slider in child)
            {
                if (slider.name == "RGBSliders")
                {
                    buttons.Add(slider.gameObject);
                }
            }
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(false);
        }*/
    }

	void Update ()
    {
        for (int i = 0; i < images.Count; i++)
        {
            colorsCurrent[i] = images[i].color;
        }

        for (int i = 0; i < images.Count; i++)
        {
            if (images[i].transform.parent.parent.name == "RGBSliders_RingGates")
            {
                lighting.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_HyperTunnel")
            {
                hyperTunnelMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Outside")
            {
                for (int n = 0; n < particles.Count; n++)
                {
                    particles[n].startColor = colorsCurrent[i];
                }
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Wireframes")
            {
                wireframeMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Hull")
            {
                shipMaterial.color = colorsCurrent[i];
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Shields")
            {
                shield.startColor = new Color32(colorsCurrent[i].r,colorsCurrent[i].g,colorsCurrent[i].b,170);
            }
            else if (images[i].transform.parent.parent.name == "RGBSliders_Thruster")
            {
                
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
            colorsInMemory.Add(colorsCurrent[i]);
        }
    }
}
