using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour {

    // Set negative if points should be reduced
    public int itemScorePoints;

    private Canvas levelUI;
    private LevelTimer timer;
    //private Health hpCounter;


    void Awake()
    {
        if (GameObject.FindWithTag("levelTimer") != null)
        {
            timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
        }
        else
        {
            Debug.Log("Item found no timer!");
        }

        if (FindObjectOfType<Canvas>() != null)
        {
            levelUI = FindObjectOfType<Canvas>();

            if (levelUI.tag != "gameLevelUI")
            {
                Debug.Log("Item found wrong UI!");
            }
        }

        //hpCounter = GameObject.FindObjectOfType<Health>();
    }


    public void HitObstacle(bool destroyWhenHit)
    {
        if (levelUI != null)
        {
            levelUI.GetComponent<FloatPointUI>().GeneratePoints(-itemScorePoints);
        }
        if (timer != null)
        {
            timer.AddScore(-1 * itemScorePoints);
            timer.ContinueBonusStreak(false);
        }
        //hpCounter.Damage();
        if (destroyWhenHit)
        {
            Destroy(this.gameObject);
        }
    }
}
