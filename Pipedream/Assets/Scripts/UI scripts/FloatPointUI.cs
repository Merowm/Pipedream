using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatPointUI : MonoBehaviour 
{
    public Text floatingPoints;
    private Text points;

    public void GeneratePoints(int scoreAmount)
    {
        points = Instantiate(floatingPoints, this.transform.position, Quaternion.identity) as Text;
        points.rectTransform.SetParent(this.gameObject.transform, false); 
        
        points.rectTransform.localPosition.Set(0, -80, 0);
        points.GetComponent<Text>().text = scoreAmount.ToString();
    }
}
