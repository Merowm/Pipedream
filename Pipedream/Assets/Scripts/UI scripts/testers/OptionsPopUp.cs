using UnityEngine;
using System.Collections;

public class OptionsPopUp : MonoBehaviour {
    public int menuSceneNumber;
    public bool isActive;
    public bool isSoundOn;
    public bool isMusicOn;
    public Texture2D optionsButton;
    public Texture2D closeOptionsButton;
    public Texture2D soundOn;
    public Texture2D soundOff;
    public Texture2D musicOn;
    public Texture2D musicOff;
    public Texture2D goToMenuButton;

    private Rect optionsButtonRect;
    private Rect optionsPopUpRect;
    private Rect soundButtonRect;
    private Rect musicButtonRect;
    private Rect goToMenuRect;
    private Rect closeOptionsRect;
	// Use this for initialization
	void Awake ()
    {
        optionsButtonRect = new Rect(Screen.width - 40, 0, 40, 40);
        optionsPopUpRect = new Rect(Screen.width - 100, 0, 100, 100);
        soundButtonRect = new Rect(Screen.width - 45, 15, 30, 30);
        musicButtonRect = new Rect(Screen.width - 85, 15, 30, 30);
        goToMenuRect = new Rect(Screen.width - 85, 55, 30, 30);
        closeOptionsRect = new Rect(Screen.width - 45, 55, 30, 30);
    }
	
    void OnGUI()
    {
        if (!isActive)
        {
            if (GUI.Button(optionsButtonRect, optionsButton))
            {
                // TODO: Pause game!
                isActive = true;
            }
        }
        else
        {
            // create options menu

            GUI.Box(optionsPopUpRect, "Options");

            if (isSoundOn)
            {
                ToggleButton(soundButtonRect, soundOn, ref isSoundOn);
            }
            else
            {
                ToggleButton(soundButtonRect, soundOff, ref isSoundOn);
            }

            if (isMusicOn)
            {
                ToggleButton(musicButtonRect, musicOn, ref isMusicOn);
            }
            else
            {
                ToggleButton(musicButtonRect, musicOff, ref isMusicOn);
            }

            if (GUI.Button(goToMenuRect, goToMenuButton))
            {
                Application.LoadLevel(menuSceneNumber);
            }

            if (GUI.Button(closeOptionsRect, closeOptionsButton))
            {
                // TODO: Unpause game!
                isActive = false;
            }
        }
    }
    void ToggleButton(Rect buttonPosition, Texture2D buttonTexture, ref bool activeState)
    {
        if (GUI.Button(buttonPosition, buttonTexture))
        {
            activeState = !activeState;
        }
    }
}
