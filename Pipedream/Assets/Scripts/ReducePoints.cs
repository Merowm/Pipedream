using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour {

    // Set negative if points should be reduced
    public int itemScorePoints;

    private Canvas levelUI;
    private CountScore score;
    private Statistics stats;


    void Awake()
    {
        score = FindObjectOfType<CountScore>();
        stats = FindObjectOfType<Statistics>();
        levelUI = FindObjectOfType<Canvas>();
        if (levelUI.tag != "gameLevelUI")
        {
            Debug.Log("Item found wrong UI!");
        }
    }


    public void HitObstacle(bool destroyWhenHit)
    {
        levelUI.GetComponent<FloatPointUI>().GeneratePoints(-itemScorePoints);
        stats.AddToHitCount();
        score.AddScore( -1 * itemScorePoints);
        score.ContinueBonusStreak(false);
        if (destroyWhenHit)
        {
            Destroy(this.gameObject);
        }
    }
}
