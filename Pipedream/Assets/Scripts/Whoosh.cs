using UnityEngine;
using System.Collections;

public class Whoosh : MonoBehaviour {

    AudioClip sound;
    VolControl volCtrl;

    void Awake()
    {
        volCtrl = FindObjectOfType<VolControl>();
        if (volCtrl != null)
        {
            sound = volCtrl.whooshSound;
        }
    }
    void OnTriggerEnter(Collider coll){
        AudioSource.PlayClipAtPoint(sound, transform.position, volCtrl.effectVol * 0.3f * volCtrl.masterVol);
    }

}
