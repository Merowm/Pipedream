using UnityEngine;
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
                Debug.Log("menu fade happening");
                sound.menuMusic.volume -= 0.05f;
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
        Debug.Log("loader heard");
        loadingDone = false;
        nextlevel = leveltoload;
        isMenu = isMenuScene;
        
        screenImage.SetActive(true);
    }
    public void hideLoader()
    {
        screenImage.SetActive(false);
    }
}
