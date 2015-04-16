using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatPointUI : MonoBehaviour 
{
    //public Canvas floatingPoints;
    public GameObject pointsImage;
    private Canvas points;
    private GameObject pointImg;
    Vector3 pointPosition;
    //public Vector3 offset;

    int cookieclickcount = 0;
    void Start()
    {
        pointPosition = new Vector3(0, 120, 0);
    }

    public void GeneratePoints(int scoreAmount)
    {
        // TODO: change position so it doesn't obstruct pipe view!
        //pointPosition = Camera.main.transform.position;
        //points = Instantiate(floatingPoints, pointPosition, Quaternion.identity) as Canvas;
        //points.transform.SetParent(Camera.main.transform, false);
        //points.transform.localPosition = offset;
        pointImg = Instantiate(pointsImage, pointPosition, Quaternion.identity) as GameObject;
        pointImg.transform.SetParent(this.transform);
        pointImg.transform.localPosition = pointPosition;
        pointImg.GetComponent<Text>().text = scoreAmount.ToString();

    }
    public void GeneratePointImage(int scoreAmount)
    {
        cookieclickcount++;
        pointImg = Instantiate(pointsImage, pointPosition, Quaternion.identity) as GameObject;
        pointImg.transform.SetParent(this.transform);
        pointImg.transform.localPosition = pointPosition;
        pointImg.GetComponent<Text>().text = cookieclickcount.ToString();
    }

}
