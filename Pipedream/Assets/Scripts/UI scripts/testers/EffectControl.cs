using UnityEngine;
using System.Collections;

public class EffectControl : MonoBehaviour {

    ParticleSystem warpForm;
    ParticleSystem speedUp;
    ParticleSystem hyper;
    ParticleSystem slowDown;
    
    GameObject tunnel;

    float time;
    bool hasJumped;    
    bool inHyper;

	void Start ()
    {
        warpForm = GameObject.Find("warpFormEffect").GetComponent<ParticleSystem>();
        speedUp = GameObject.Find("jumpEffect").GetComponent<ParticleSystem>();
        slowDown = GameObject.Find("slowdownEffect").GetComponent<ParticleSystem>();
        hyper = GameObject.Find("hyperTubeEffect").GetComponent<ParticleSystem>();
        hasJumped = false;
        
        time = 0;        
	}
	
	void Update () 
    {
        time += Time.deltaTime;
        if (!hasJumped && time >= 3.8f)
            Warp();
        // Back to normal space criteria here. NB! slowing effect has to be called earlier.

	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HyperspaceGateExit")
        {
            SlowDown();
        }
    }
    void Warp()
    {
        warpForm.Play();
        speedUp.Play();      
        hasJumped = true;
        inHyper = true;
        Invoke("SetHyperEffect", 1.2f);
    }
    void SlowDown()
    {        
        slowDown.Play();
        inHyper = false;
        Invoke("SetHyperEffect", 0.0f);        
    }
    void SetHyperEffect()
    {
        hyper.enableEmission = inHyper;        
    }
}
