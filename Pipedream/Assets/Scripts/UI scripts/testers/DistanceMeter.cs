using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceMeter : MonoBehaviour {

    public GameObject marker;
    GameObject[] entrances; // TODO: uniform gate tag for easier handling?
    GameObject[] exits;
    List<GameObject> gates;
    float fullrun;
    float donerun;
    float[] gateX;
    GameObject area;
    List<GameObject> mark;
    Vector3 pos;
    GameObject temp;
    float areaWidth;
	
	void Start ()
    {
        gates = new List<GameObject>();        
        area = GameObject.Find("markerarea");
        areaWidth = area.GetComponent<RectTransform>().rect.width;
        Debug.Log(area);
        entrances = GameObject.FindGameObjectsWithTag("EntranceGate");
        if (entrances != null)
            Debug.Log("found " + entrances.Length + " gates");
        exits = GameObject.FindGameObjectsWithTag("ExitGate");
        foreach (GameObject e in entrances)
        {
            gates.Add(e);
        }
        foreach (GameObject f in exits)
        {
            gates.Add(f);
        }
        gateX = new float[gates.Count];
        for (int i = 0; i < gates.Count; ++i)
        {
            gateX[i] = donerun + gates[i].transform.position.z;
            donerun = gateX[i];
        }

        ConvertToPanelSize();
        SetMarkers();
	}

    void ConvertToPanelSize()
    {
        float rate = areaWidth / fullrun;
        for (int i = 0; i < gateX.Length; ++i)
        {
            gateX[i] *= rate;

        }
    }
	
    void SetMarkers()
    {
        mark = new List<GameObject>();

        for (int i = 0; i < gateX.Length; ++i)
        {
            pos = new Vector3(gateX[i] - (areaWidth/2), 0, 0);
            Debug.Log("mark at " + gateX[i]);            
            temp = Instantiate(marker) as GameObject;
            temp.transform.SetParent(area.GetComponent<RectTransform>());
            temp.transform.localPosition = pos;
            Debug.Log("placed at " + pos);
            mark.Add(temp);
        }
    }

    public void SendLength(float runlength)
    {
        fullrun = runlength;
    }
}
