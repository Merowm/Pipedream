using UnityEngine;
using System.Collections;

public class LoadScreen : MonoBehaviour {

    GameObject screenImage;
    string nextlevel;
    VolControl sound;
    bool loadingDone;

	void Start()
    {
        sound = GetComponent<VolControl>();
        screenImage = GameObject.Find("loadOverlay");
        screenImage.SetActive(false);
        loadingDone = true;
	}

	void Update () 
    {
        if (!loadingDone)
        {
            if (Application.loadedLevelName == nextlevel && !Application.isLoadingLevel)
            {
                hideLoader();
                AudioSource music = Camera.main.GetComponent<AudioSource>();
                //sound.music = music;
                
                music.volume = sound.musicMaxVol;
                Time.timeScale = 1;
                loadingDone = true;
            }
        }
	}

    public void showLoader(string leveltoload)
    {
        loadingDone = false;
        nextlevel = leveltoload;
        screenImage.SetActive(true);
    }
    public void hideLoader()
    {
        screenImage.SetActive(false);
    }
}
