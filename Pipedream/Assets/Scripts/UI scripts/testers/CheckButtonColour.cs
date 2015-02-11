using UnityEngine;
using System.Collections;

public class CheckButtonColour : MonoBehaviour {

    public bool achieved;

    Rect labelPos;
    public GameObject button;
	// Use this for initialization
	void Start ()
    {
        button = this.gameObject;
        Vector3 pos = button.transform.position;// Camera.main.WorldToScreenPoint(button.transform.position);
	    if (achieved)
        {
            renderer.material.color = Color.cyan;
        }
        else
        {
            renderer.material.color = Color.magenta;
        }
        labelPos = new Rect(pos.x + 30, pos.y + 30, 300, 30);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnGUI()
    {
        GUI.Label(labelPos, "Achievement achieved");
    }
}
