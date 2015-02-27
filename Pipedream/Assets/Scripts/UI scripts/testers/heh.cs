using UnityEngine;
using System.Collections;

public class heh : MonoBehaviour {
    
    [Range (0,255)]public float al;
    CanvasRenderer c;
    bool goup;
    float dt;
	// Use this for initialization
	void Start () 
    {
        c = GetComponent<CanvasRenderer>();
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        dt = Time.deltaTime;
        if (goup)
        {
            al += dt;

        }

        else al -= dt;
        if (al >= 255)
            goup = false;
        c.SetAlpha(al);
	}
}
