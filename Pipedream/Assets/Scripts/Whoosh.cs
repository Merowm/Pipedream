using UnityEngine;
using System.Collections;

public class Whoosh : MonoBehaviour {

    public AudioClip sound;
    VolControl volCtrl;

    void Start()
    {
        volCtrl = FindObjectOfType<VolControl>();
    }
    void OnTriggerEnter(Collider coll){
        AudioSource.PlayClipAtPoint(sound, transform.position, volCtrl.effectVol);
    }

}
