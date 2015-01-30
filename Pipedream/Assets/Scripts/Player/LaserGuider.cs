using UnityEngine;
using System.Collections;
//TODO:Unfinished product, the laser unwantedly sticks to objects
public class LaserGuider : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private RaycastHit hit;

	void Awake ()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
	}

	void Update ()
    {
        lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position,new Vector3(0,0,1),out hit,Mathf.Infinity))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
	}
}
