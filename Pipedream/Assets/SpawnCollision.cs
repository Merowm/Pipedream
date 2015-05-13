using UnityEngine;
using System.Collections;

public class SpawnCollision : MonoBehaviour
{
    public bool colliding = false;

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag != "Whoosh")
        //{
            Debug.Log("heieheiehie");
            Debug.Break();
            colliding = true;
            Vector3 rotation = new Vector3(0,0,Random.Range(0.0f,360.0f));
            transform.parent.parent.transform.Rotate(rotation);
       // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Whoosh")
        {
            //colliding = false;
        }
    }
}
