using UnityEngine;
using System.Collections;

public class barPlacer : MonoBehaviour {

    public float x;
    public float y;
    Vector3 placing;
    Vector2 scrsize;
	// Use this for initialization
	void Start () 
    {        
        scrsize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        placing = new Vector3(x * scrsize.x, y * scrsize.y, 10);
        this.transform.localPosition = Camera.main.ScreenToWorldPoint(placing);
	}
	

}
