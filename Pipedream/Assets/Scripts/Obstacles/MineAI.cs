using UnityEngine;
using System.Collections;

public class MineAI : MonoBehaviour
{
    public float speedForward = 100.0f;
    public float speed2D = 5.0f;
    public float followingDistance = 100.0f;
    public GameObject target;
    public GameObject targetPositionObject; //Where the target will be at, on the xy-axis,
    public Vector3 targetPosition; //When the target's z-axis aligns with that of the following object

    private Transform bodyTransform;
    private Vector3 direction;

	void Awake ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        bodyTransform = transform.GetChild(0).transform;
        targetPosition = targetPositionObject.transform.position;
	}

	void FixedUpdate ()
    {
        //If the target is close enough on the z-axis, continue
        if (followingDistance > Vector3.Distance(new Vector3(0,0,transform.position.z),
                                                 new Vector3(0,0,target.transform.position.z)))
        {
            //Setting target position
            targetPosition = new Vector3(target.transform.position.x,target.transform.position.y,targetPosition.z);
            //Setting targetPositionObject's position
            targetPositionObject.transform.position = targetPosition;
            //Direction calculation
            direction = (targetPosition - bodyTransform.position).normalized;

            //If target is not yet passed the mine, continue
            if (transform.position.z >= target.transform.position.z - 50.0f)
            {
                //Moves the mine
                bodyTransform.position += direction * speed2D * Time.deltaTime;
            }
        }
	}

    void ForwardMovement ()
    {
        Vector3 lastPosition = transform.position;
        transform.position += direction * speedForward * Time.deltaTime;
        Vector3 currentPosition = transform.position;
    }

    void OnDrawGizmos ()
    {
        if (followingDistance > Vector3.Distance(new Vector3(0, 0, transform.position.z),
                                                 new Vector3(0, 0, target.transform.position.z)))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, targetPosition);
            Gizmos.color = Color.white;
        }
    }
}
