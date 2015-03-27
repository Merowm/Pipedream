using UnityEngine;
using System.Collections;

public class MineSticking : MonoBehaviour
{
    public GameObject target; //Same as in mineAI
    public float countdownTimer = 5.0f;
    public bool stickToTarget = false; //Makes mine stick to the target when true
    public bool stuckToTarget = false; //Whether or not the mine is continuously stuck to the target, true = stuck

    private Health health;
    private Transform bodyTransform;
    private Vector3 stuckPosition;

	void Awake ()
    {
        target = transform.GetComponent<MineAI>().target;
        health = target.GetComponent<Health>();
        bodyTransform = transform.GetChild(0).transform;
	}

	void FixedUpdate ()
    {
	    if (stickToTarget)
        {
            //Set stuckPosition
            stuckPosition = target.transform.position;
            //Disable the mine's MineAI script
            transform.GetComponent<MineAI>().enabled = false;
            //Set booleans
            stickToTarget = false;
            stuckToTarget = true;
        }

        if (stuckToTarget)
        {
            //Update stuckPosition and mine's position with it
            stuckPosition = target.transform.position;
            bodyTransform.position = stuckPosition;
            //Update mine's parent's position and rotation
            transform.position = target.transform.parent.position;
            transform.rotation = target.transform.parent.rotation;

            Countdown();
        }
	}

    void Countdown ()
    {
        //Countdown
        countdownTimer -= Time.deltaTime;

        //When timer at 0 -> EXPLOSION
        if (countdownTimer <= 0)
        {
            Explosion();
        }
    }

    void Explosion ()
    {
        health.Damage();
        Destroy(gameObject);
    }
}
