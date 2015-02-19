using UnityEngine;
using System.Collections;

public class scrollbartest : MonoBehaviour {


    public float minx;
    public float maxx;
    float range;
    Vector3 scale;
    float scale_x;

    void Awake()
    {
        range = maxx - minx;
        scale = new Vector3(minx, 1, 1);
        transform.localScale = scale;
    }
    public void MoveSlider(float fillPercentage)
    {
        scale_x = minx + (fillPercentage * range);
        scale.x = scale_x;
        transform.localScale = scale;
    }
}
