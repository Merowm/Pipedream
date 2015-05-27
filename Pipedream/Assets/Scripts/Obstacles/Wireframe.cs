using UnityEngine;
using System.Collections;

public class Wireframe : MonoBehaviour
{
    public float displayDistance = 100.0f;
    public float asteroidSpeedMultiplier = 3.0f;
    public bool customizationObject = false;

    private Transform player;
    private MovementForward playerMovement;
    private Transform wireframe;
    private Transform asteroid;
    private HyperTunnelMovement parentMovement; //Used in customization
    private float distance = 0.0f;
    private float asteroidSpeedPerSecond;
    private bool positionSet = false;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();
        wireframe = transform.GetChild(0).transform;
        asteroid = wireframe.FindChild("AsteroidGraphics").transform;
    }

    void Start ()
    {
        if (customizationObject)
        {
            parentMovement = transform.parent.parent.GetComponent<HyperTunnelMovement>();
        }
    }
    
    void FixedUpdate ()
    {
        if (MovementForward.inHyperspace)
        {
            if (transform.position.z > player.transform.position.z)
            {
                distance = Vector3.Distance(new Vector3(0,0,player.transform.position.z),
                                            new Vector3(0,0,transform.position.z));

                float playerSpeedPerSecond = 0.0f;

                if (playerMovement.currentSpeedPerSecond > 0)
                {
                    playerSpeedPerSecond = playerMovement.currentSpeedPerSecond;
                }
                else
                {
                    //Used in customization
                    if (parentMovement != null)
                    {
                        playerSpeedPerSecond = -parentMovement.speedPerSecond;
                    }
                }

                asteroidSpeedPerSecond = playerSpeedPerSecond * asteroidSpeedMultiplier;

                if (transform.position.z <= player.transform.position.z + playerSpeedPerSecond)
                {
                    if (distance <= displayDistance)
                    {
                        asteroid.GetComponent<MeshRenderer>().enabled = true;
                    }

                    setPosition();

                    float z = asteroid.position.z;
                    z -= asteroidSpeedPerSecond * Time.deltaTime;
                    asteroid.position = new Vector3(asteroid.position.x,asteroid.position.y,z);
                }
            }
            else
            {
                asteroid.GetComponent<MeshRenderer>().enabled = false;
                asteroid.position = wireframe.position;
                positionSet = false;
            }
        }
    }

    void setPosition ()
    {
        if (!positionSet)
        {
            asteroid.position = new Vector3(asteroid.position.x,
                                            asteroid.position.y,
                                            wireframe.position.z + asteroidSpeedPerSecond);
            positionSet = true;
        }
    }
}
