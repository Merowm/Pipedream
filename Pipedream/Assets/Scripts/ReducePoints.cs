using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour {

    // Set negative if points should be reduced
    public int itemScorePoints;
    public Transform floatingPoints;
    

    private CountScore score;
    private Statistics stats;

    void Awake()
    {
        score = FindObjectOfType<CountScore>();
        stats = FindObjectOfType<Statistics>();
    }


    public void HitObstacle(bool destroyWhenHit)
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
