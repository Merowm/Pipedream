using UnityEngine;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour 
{
    public int itemScorePoints;
    public Transform textObject;

	private GameObject score;
    private GameObject pointText;
	void Awake ()
    {
		score = GameObject.FindGameObjectWithTag ("Scoretext");
	}
	
	
	void OnTriggerEnter(Collider other)
	{
        // if wrong text flashes, set active after setting text!
        pointText = Instantiate(textObject, this.transform.position, Quaternion.identity) as GameObject;
        pointText.GetComponent<TextMesh>().text = itemScorePoints.ToString();
		score.GetComponent<CountScore>().AddScore(itemScorePoints);
        Camera.main.GetComponent<MusicVolumeReset>().hasCollectedItem = true;
		Destroy (this.gameObject);
	}
}
