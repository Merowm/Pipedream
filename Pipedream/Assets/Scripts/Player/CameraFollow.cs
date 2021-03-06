﻿using UnityEngine;
using System.Collections;
//TODO:Remove bug creating unwanted acceleration
public class CameraFollow : MonoBehaviour
{
    public float distanceFromTarget = 15.0f;
    public float position = 9.0f;
	public float dampTime = 0.3f;
	public Transform target;

    private Transform rotator;
    private Transform mainCamera;
	private Vector3 velocity = Vector3.zero;
    private Transform effectSys;

	void Awake ()
	{
        if (target)
        {
            rotator = transform.GetChild(0).transform;
            mainCamera = rotator.GetChild(0).transform;
            effectSys = GameObject.FindGameObjectWithTag("effects").transform;
        }
	}

	void FixedUpdate ()
	{
		if (target)
		{
            transform.position = target.parent.position;

            //if (MovementForward.inHyperspace)
            //{
                /*mainCamera.position = Vector3.SmoothDamp(mainCamera.position,
                                                         //new Vector3(target.position.x,target.position.y + spacePosition,target.position.z - distanceFromTarget),
                                                         new Vector3(transform.position.x - ((transform.position.x - target.position.x) / hyperPosition),
                            transform.position.y - ((transform.position.y - target.position.y) / hyperPosition),
                            target.position.z - distanceFromTarget),
                                                         ref velocity,//<--?
                                                         dampTime);*/






                //mainCamera.position = new Vector3((target.parent.position.x + target.position.x) / hyperPosition,
                  //                                (target.parent.position.y + target.position.y) / hyperPosition,
                    //                              target.position.z - distanceFromTarget);
                mainCamera.position = new Vector3(transform.position.x - ((transform.position.x - target.position.x) / position),
                                                  transform.position.y - ((transform.position.y - target.position.y) / position),
                                                  target.position.z - distanceFromTarget);
                //transform.Rotate(Vector3.forward *
                 //                Time.deltaTime *
                  //              (target.parent.GetComponent<Movement2D>().currentRotationSpeed * 10));
                rotator.rotation = target.parent.rotation;
                effectSys.rotation = new Quaternion(0, 0, -rotator.rotation.z, 0);

            /*}
            else
            {
                rotator.rotation = new Quaternion(0,0,0,0);
                //Vector3 delta = target.position - mainCamera.camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
                //Vector3 destination = mainCamera.position + delta;
                mainCamera.position = Vector3.SmoothDamp(mainCamera.position,
                                                         new Vector3(target.position.x,target.position.y + spacePosition,target.position.z - distanceFromTarget),
                                                         ref velocity,//<--?
                                                         dampTime);
                    
                //mainCamera.position = new Vector3(target.position.x,
                //                                  target.position.y + 2,
                //                                  target.position.z - distanceFromTarget);
            }*/
		}
	}
}
