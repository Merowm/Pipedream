using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    public float followingDistance = 100.0f;
    public GameObject target;
    public GameObject targetPositionObject; //Where the target will be at, on the xy-axis,
    public Vector3 targetPosition; //when the target's z-axis aligns with that of the following object

    private Vector3 direction;

	void Awake ()
    {
        targetPosition = targetPositionObject.transform.position;
	}

	void FixedUpdate ()
    {
        if (followingDistance > Vector3.Distance(new Vector3(0,0,transform.parent.transform.position.z),
                                                 new Vector3(0,0,target.transform.position.z)))
        {
            targetPosition = new Vector3(target.transform.position.x,target.transform.position.y,targetPosition.z);
            targetPositionObject.transform.position = targetPosition;

            direction = (targetPosition - transform.position).normalized;

            if (transform.parent.position.z >= target.transform.position.z - 50.0f)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
        }
	}

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, targetPosition);
        Gizmos.color = Color.white;
    }
}
