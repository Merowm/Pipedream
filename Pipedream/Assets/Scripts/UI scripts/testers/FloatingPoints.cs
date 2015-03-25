using UnityEngine;
using System.Collections;

public class FloatingPoints : MonoBehaviour
{

    public Vector3 offset;
    public float lifeTime;

    private float dt;
    private TextMesh textMesh;
    private float lifeTimer;

	void Awake ()
    {
        textMesh = this.GetComponent<TextMesh>();
        lifeTimer = lifeTime;
        //offset = new Vector3(0, 1, 0);
	}
	
	void Update () 
    {
        dt = Time.deltaTime;
        transform.position += offset * dt;
        textMesh.GetComponent<Renderer>().material.color = DeltaFade(textMesh.GetComponent<Renderer>().material.color, dt);
        lifeTimer -= dt;
        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    Color DeltaFade(Color color, float _dt)
    {
        Color temp = color;
        temp.a -= 1 / lifeTime * _dt;
        color = temp;
        return color;
    }
}
