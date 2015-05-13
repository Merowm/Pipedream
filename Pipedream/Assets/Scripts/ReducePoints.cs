using UnityEngine;
using System.Collections;

public class ReducePoints : MonoBehaviour
{

    // Set negative if points should be reduced
    public int itemScorePoints = 100;

    private Canvas levelUI;
    private LevelTimer timer;
    private VolControl volCtrl;
    private Health health;

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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponentInChildren<Health>();
    }

    void Start()
    {
        itemScorePoints = (int)(itemScorePoints * MovementForward.difficultyMultiplier);
    }

    public void HitObstacle(bool disableWhenHit)
    {
        if (!health.invulnerable)
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
        }
        volCtrl.TestCrashEffect(this.transform.position);
        if (disableWhenHit)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
