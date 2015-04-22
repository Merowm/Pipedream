using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScreen : MonoBehaviour {

    public GameObject screenImage;
    string nextlevel;
    VolControl sound;
    bool loadingDone;
    public bool isMenu;

	void Start()
    {
        sound = GetComponent<VolControl>();
        //screenImage = GameObject.Find("loadOverlay");
        //screenImage.SetActive(false);
        loadingDone = true;
        isMenu = true;
	}

	void Update () 
    {
        if (!loadingDone)
        {
            if (!isMenu)
            {
                sound.menuMusic.volume -= 0.1f;
            }
            if (Application.loadedLevelName == nextlevel && !Application.isLoadingLevel)
            {
                sound = GetComponent<VolControl>();
                hideLoader();
                //AudioSource music = Camera.main.GetComponent<AudioSource>();
                sound.SetMusicType(isMenu);
                //music.volume = sound.musicMaxVol;
                Time.timeScale = 1;
                loadingDone = true;
            }
        }
	}

    public void showLoader(string leveltoload, bool isMenuScene)
    {
        loadingDone = false;
        nextlevel = leveltoload;
        isMenu = isMenuScene;

        if (!isMenu)
        screenImage.SetActive(true);       
    }

    public void hideLoader()
    {
        screenImage.SetActive(false);
    }
}
