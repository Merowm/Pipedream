using UnityEngine;
using System.Collections;

public class ObstacleUpDown : MonoBehaviour {

    public float maxDistance = 1;
    public float movedDistanceSqr = 0;

    public float maxVelocity = 5;
    
    public float displayDistance = 400.0f;
    private Transform player;

    enum STATE{
        IDLE,
        UP,
        DOWN
    }
    STATE currentState = STATE.IDLE;


    void Awake(){        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void FixedUpdate () {
        switch(currentState){
            case STATE.IDLE:
                UpdateIdle();
                break;
            case STATE.UP:
                UpdateUp();
                break;
            case STATE.DOWN:
                UpdateDown();
                break;
        }	    
	}

    void UpdateIdle(){
        if (displayDistance * displayDistance > 
            Vector3.Distance(new Vector3(0.0f, 0.0f, transform.position.z), 
                         new Vector3(0.0f, 0.0f, player.position.z)))
        {
            currentState = STATE.UP;
        }
    }

    void UpdateUp(){
        transform.GetChild(0).transform.position += transform.up * maxVelocity * Time.deltaTime;
        movedDistanceSqr += (transform.up * maxVelocity * Time.deltaTime).sqrMagnitude;
        if (movedDistanceSqr >= maxDistance * maxDistance)
        {
            currentState = STATE.DOWN;
            movedDistanceSqr = 0;
        }
    }

    void UpdateDown(){
        transform.GetChild(0).transform.position -= transform.up * maxVelocity * Time.deltaTime;
        movedDistanceSqr += (transform.up * maxVelocity * Time.deltaTime).sqrMagnitude;
        if (movedDistanceSqr >= maxDistance * maxDistance)
        {
            currentState = STATE.UP;
            movedDistanceSqr = 0;
        }
    }
}
