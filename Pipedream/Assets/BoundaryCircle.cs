using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BoundaryCircle : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;

    void Start ()    
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount (segments + 1);
        line.useWorldSpace = false;
        CreatePoints ();
    }

    void CreatePoints ()
    {
        float x;
        float y;
        float z = 0f;
        float angle = 20.0f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition (i,new Vector3(x,y,z) );
            angle += (360f / segments);
        } 
    }

    /*
    private LineRenderer lineRenderer;
    private int lengthOfLineRenderer = 20;

	void Awake ()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();

        lineRenderer.SetVertexCount(lengthOfLineRenderer);


	}

	void Update ()
    {
        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            Vector3 pos = new Vector3(, , 0);
            lineRenderer.SetPosition(i, pos + transform.position);
        }
	}

    void GetPosition ()
    {

    }

    void OnDrawGizmos ()
    {
        //Gizmos.DrawLine(transform.position, origin.position);
    }*/
}
