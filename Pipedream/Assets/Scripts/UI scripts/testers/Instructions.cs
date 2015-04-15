using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Instructions : MonoBehaviour {

    public GameObject[] tutPics;
    public float showTime;
    GameObject current;
    //CanvasGroup tutPanel;

    float timer;
    float dt;
    int num;
	
	void Start ()
    {
        timer = 0;
        num = 0;
        current = tutPics[num];
        current.SetActive(true);
	}
    // simple looping through tutorial slides
	void Update ()
    {
        dt = Time.deltaTime;
        timer += dt;
        if (timer >= showTime)
        {
            current.SetActive(false);
            num++;
            if (num >= tutPics.Length)
                num = 0;
            current = tutPics[num];
            current.SetActive(true);
            timer = 0;
        }

	}
    // TODO: fading?
    void FadeOld(float deltatime)
    {
        //tutPanel.alpha -= 0.5f * deltatime;
    }
}
