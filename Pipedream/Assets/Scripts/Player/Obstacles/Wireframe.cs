using UnityEngine;
using System.Collections;

public class Wireframe : MonoBehaviour
{
    public float displayDistance = 100.0f;
    public float asteroidSpeedMultiplier = 3.0f;

    private Transform player;
    private MovementForward playerMovement;
    private Transform wireframe;
    private Transform asteroid;
    private float distance = 0.0f;
    private float asteroidSpeedPerSecond;
    private bool positionSet = false;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();
        wireframe = transform.GetChild(0).transform;
        asteroid = wireframe.FindChild("Asteroid").transform;
    }
    
    void FixedUpdate ()
    {
        if (MovementForward.inHyperSpace)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            asteroidSpeedPerSecond = playerMovement.currentSpeedPerSecond * asteroidSpeedMultiplier;

            if (transform.position.z <= player.transform.position.z + playerMovement.currentSpeedPerSecond)
            {
                if (distance <= displayDistance)
                {
                    asteroid.GetComponent<MeshRenderer>().enabled = true;
                    asteroid.GetComponent<Animator>().enabled = true;
                }

                setPosition();

                float distanceToTravel = asteroidSpeedPerSecond;
                float z = asteroid.position.z;
                z -= asteroidSpeedPerSecond * Time.deltaTime;

                asteroid.position = new Vector3(asteroid.position.x,asteroid.position.y,z);

                if (transform.position.z <= player.transform.position.z)
                {
                    //asteroid.GetComponent<MeshRenderer>().enabled = false;
                    //asteroid.GetComponent<Animator>().enabled = false;
                    asteroid.position = wireframe.position;
                    //Destroy(transform.gameObject);
                }
            }
        }
    }

    void setPosition()
    {
        if (!positionSet)
        {
            asteroid.position = new Vector3(asteroid.position.x,
                                            asteroid.position.y,
                                            wireframe.position.z + asteroidSpeedPerSecond);
            positionSet = true;
            Debug.Log(wireframe.position.z + asteroidSpeedPerSecond);
        }
    }
}
