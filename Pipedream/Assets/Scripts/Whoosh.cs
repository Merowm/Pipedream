using UnityEngine;
using System.Collections;

public class Whoosh : MonoBehaviour {

    AudioClip sound;
    VolControl volCtrl;

    void Start()
    {
        volCtrl = FindObjectOfType<VolControl>();
        sound = volCtrl.whooshSound;
    }
    void OnTriggerEnter(Collider coll){
        AudioSource.PlayClipAtPoint(sound, transform.position, volCtrl.effectVol);
    }

}
