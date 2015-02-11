using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour {

    // Set negative if points should be reduced
    public int itemScorePoints;
    public Transform floatingPoints;
    public bool destroyWhenHit;

    private CountScore score;
    private Statistics stats;

    void Awake()
    {
        score = FindObjectOfType<CountScore>();
        stats = FindObjectOfType<Statistics>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit trigger");
        Instantiate(floatingPoints, this.transform.position, Quaternion.identity);
        stats.AddToHitCount();
        score.AddScore( -1 * itemScorePoints);
        score.ContinueBonusStreak(false);
        if (destroyWhenHit)
        {
            Destroy(this.gameObject);
        }
    }
}
