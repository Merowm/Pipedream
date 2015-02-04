using UnityEngine;
using System.Collections;

public class FloatingPointsSprite : MonoBehaviour {

    public Vector3 offset;
    public float lifeTime;

    private float dt;
    private float lifeTimer;
	
	void Awake ()
    {
        lifeTimer = lifeTime;
	}
	
	void Update () 
    {
        dt = Time.deltaTime;
        transform.position += offset * dt;
        renderer.material.color = DeltaFade(renderer.material.color, dt);
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
