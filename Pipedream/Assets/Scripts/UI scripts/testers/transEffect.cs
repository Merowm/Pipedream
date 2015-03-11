using UnityEngine;
using System.Collections;

public class transEffect : MonoBehaviour {

    GameObject ship;
    ParticleSystem effect;
    MovementForward move;
	// Use this for initialization
	void Start ()
    {
        effect = this.GetComponent<ParticleSystem>();
        ship = GameObject.FindWithTag("Player");
        move = ship.GetComponent<MovementForward>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (!MovementForward.inHyperSpace)
        {
            if (move.accelerateToHyperspace)
            {
                Debug.Log("launch particles!");
                effect.Play();
            }            
        }
       
	}
}
