using UnityEngine;
using System.Collections;

public class LoadScreen : MonoBehaviour {

    GameObject screenImage;
    string nextlevel;

	void Start()
    {
        screenImage = GameObject.Find("loadOverlay");
        screenImage.SetActive(false);
	}

	void Update () 
    {
        if (Application.loadedLevelName == nextlevel && !Application.isLoadingLevel)
            hideLoader();
            
        //else
            //Debug.Log("loading is happening");
	}

    public void showLoader(string leveltoload)
    {

        nextlevel = leveltoload;
        screenImage.SetActive(true);
    }
    public void hideLoader()
    {
        screenImage.SetActive(false);
    }
}
