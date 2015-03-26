using UnityEngine;
using System.Collections;

public class Whoosh : MonoBehaviour {

    public AudioClip sound;

    void OnTriggerEnter(Collider coll){
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

}
