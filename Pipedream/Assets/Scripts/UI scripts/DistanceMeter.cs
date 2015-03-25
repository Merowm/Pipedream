using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceMeter : MonoBehaviour {

    public GameObject marker;
    GameObject[] gates; // TODO: uniform gate tag for easier handling?
    float fullrun;
    float donerun;
    float[] gateX;
    float[] markerX;
    GameObject area;
    List<GameObject> mark;
    float areaWidth;

    LevelTimer timer;

    GameObject ship;
    ParticleSystem speedup;
    ParticleSystem slowdown;
    ParticleSystem hyper;
    int gateInd;
    bool allEffectsDone;

	void Start ()
    {
        area = GameObject.Find("markerarea");
        areaWidth = area.GetComponent<RectTransform>().rect.width;
        // NB! Check that gates are added in right order!
        gates = GameObject.FindGameObjectsWithTag("HyperspaceGate");

        speedup = GameObject.Find("speedupEffect").GetComponent<ParticleSystem>();
        slowdown = GameObject.Find("slowdownEffect").GetComponent<ParticleSystem>();
        hyper = GameObject.Find("hyperEffect").GetComponent<ParticleSystem>();
        ship = GameObject.FindWithTag("Player");
        timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
        gateInd = 0;
        allEffectsDone = false;
        hyper.enableEmission = false;
        gateX = new float[gates.Length];
        markerX = new float[gates.Length];
        for (int i = 0; i < gates.Length; ++i)
        {
            gateX[i] = donerun + gates[i].transform.position.z + 70;
            donerun = gateX[i];
            markerX[i] = gateX[i];            
        }

        ConvertToPanelSize();
        SetMarkers();     
	}

    void Update()
    {
        if (!MovementForward.inHyperSpace)
        {
            Warp(speedup, 105);
        }
        else
        {
            Warp(slowdown, 260);
        }    
    }

    void Warp(ParticleSystem effect, float trigpoint)
    {
        if (!allEffectsDone && gateX[gateInd] - timer.GetCurrentDistance() < trigpoint)
            {
                Debug.Log("launch particles!");
                effect.Play();
                hyper.enableEmission = !MovementForward.inHyperSpace;
                if (gateInd < gateX.Length - 1)
                    gateInd++;
                else allEffectsDone = true;                
            }
    }

    void ConvertToPanelSize()
    {
        float rate = areaWidth / fullrun;
        for (int i = 0; i < markerX.Length; ++i)
        {
            markerX[i] *= rate;
        }
    }
	
    void SetMarkers()
    {
        mark = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < markerX.Length; ++i)
        {
            Vector3 pos = new Vector3(markerX[i] - (areaWidth/2), 0, 0);
            temp = Instantiate(marker) as GameObject;
            temp.transform.SetParent(area.GetComponent<RectTransform>());
            temp.transform.localPosition = pos;
            mark.Add(temp);
        }
    }

    public void SendLength(float runlength)
    {
        fullrun = runlength;
    }
}
