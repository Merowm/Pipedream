using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour
{

    // Set negative if points should be reduced
    public int itemScorePoints = 100;

    private Canvas levelUI;
    private LevelTimer timer;
    private VolControl volCtrl;
    //private Health hpCounter;


    void Awake()
    {
        if (GameObject.FindWithTag("levelTimer") != null)
        {
            timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
        }
        else
        {
            //Debug.Log("Item found no timer!");
        }
        Canvas[] all = FindObjectsOfType<Canvas>();
        foreach (Canvas c in all)
        {
            if (c.tag == "gameLevelUI")
                levelUI = c;
        }
        volCtrl = GameObject.FindWithTag("statistics").GetComponent<VolControl>();

        //hpCounter = GameObject.FindObjectOfType<Health>();
    }

    void Start()
    {
        itemScorePoints = (int)(itemScorePoints * MovementForward.difficultyMultiplier);
    }

    public void HitObstacle(bool disableWhenHit)
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
        volCtrl.TestCrashEffect(this.transform.position);
        //hpCounter.Damage();
        if (disableWhenHit)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
