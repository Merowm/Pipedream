using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof(Text))]
public class FPSText : MonoBehaviour {

    private Text text;

    public float updateTime = 0;
    private float updateTimer = 0;

	void Awake () {
        text = GetComponent<Text>();
	}
	
	void Update () {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateTime)
        {
            updateTimer -= updateTime;
            text.text = "FPS: " + 1 / Time.deltaTime;
        }
	}
}
