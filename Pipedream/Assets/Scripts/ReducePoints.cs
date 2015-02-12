using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour {

    // Set negative if points should be reduced
    public int itemScorePoints;

    private Canvas levelUI;
    private LevelTimer timer;


    void Awake()
    {
        timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
        if (timer == null)
        {
            Debug.Log("Item found no timer!");
        }
        levelUI = FindObjectOfType<Canvas>();
        if (levelUI.tag != "gameLevelUI")
        {
            Debug.Log("Item found wrong UI!");
        }
    }


    public void HitObstacle(bool destroyWhenHit)
    {
        levelUI.GetComponent<FloatPointUI>().GeneratePoints(-itemScorePoints);
        timer.AddScore( -1 * itemScorePoints);
        timer.ContinueBonusStreak(false);
        if (destroyWhenHit)
        {
            Destroy(this.gameObject);
        }
    }
}
