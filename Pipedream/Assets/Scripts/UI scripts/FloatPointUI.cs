using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatPointUI : MonoBehaviour 
{
    public Canvas floatingPoints;
    private Canvas points;
    Vector3 pointPosition;
    public Vector3 offset;

    public void GeneratePoints(int scoreAmount)
    {
        // TODO: change position so it doesn't obstruct pipe view!
        pointPosition = Camera.main.transform.position;
        points = Instantiate(floatingPoints, pointPosition, Quaternion.identity) as Canvas;
        points.transform.SetParent(Camera.main.transform, false);
        points.transform.localPosition = offset;
        points.GetComponent<Text>().text = scoreAmount.ToString();

    }
}
