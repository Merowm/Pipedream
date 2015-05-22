using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SetColors : MonoBehaviour
{
    public Material hyperTunnelMaterial;
    public Material shipHullMaterial;
    public Material shipMarkingsMaterial;
    public Material shipThrusterMaterial;
    public Material wireframeMaterial;
    public Material orbMaterial;
    
    public ChangeLighting lighting;
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    public ParticleSystem shield;
    public ParticleSystem thruster;

    private Statistics statistics;
    public Color32[] colors;

	void Awake ()
    {
        statistics = GameObject.FindGameObjectWithTag("statistics").transform.GetComponent<Statistics>();
        colors = statistics.colors;

        lighting = GameObject.FindGameObjectWithTag("MovingHyperPart").GetComponent<ChangeLighting>();
        particles = GameObject.FindGameObjectWithTag("effects").GetComponentsInChildren<ParticleSystem>().ToList();
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].transform.name == "starsystem")
            {
                particles.Remove(particles[i]);
            }
        }
        shield = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Ship").
            FindChild("Shield Particle System").GetComponent<ParticleSystem>();
        thruster = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Ship").
            FindChild("ThrusterParticles").GetComponent<ParticleSystem>();

        //Set color for hyper tunnel
        hyperTunnelMaterial.color = colors[0];
        //Set color for rings
        lighting.color = colors[1];
        //Set color for outside particles
        for (int n = 0; n < particles.Count; n++)
        {
            particles[n].startColor = colors[2];
        }
        //Set color for ship's hull
        shipHullMaterial.color = colors[3];
        //Set color for ship's markings
        shipMarkingsMaterial.color = colors[4];
        //Set color for ship's shield
        shield.startColor = new Color32(colors[5].r,colors[5].g,colors[5].b,170);
        //Set color for ship's thruster
        shipThrusterMaterial.color = colors[6];
        thruster.startColor = new Color32(colors[6].r,colors[6].g,colors[6].b,255);
        //Set color for wireframes
        wireframeMaterial.color = colors[7];
        //Set color for orbs
        orbMaterial.color = colors[8];
	}
}
