using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour
{
    public int itemScorePoints;
    public bool thisIsSpecial;
    // For debugging:
    public int bonusItemNumber;

    public Canvas levelUI;
    public LevelTimer timer;

    VolControl volCtrl;

    void Awake()
    {
        levelUI = FindObjectOfType<Canvas>();

        Canvas[] all = FindObjectsOfType<Canvas>();
        foreach (Canvas c in all)
        {
            if (c.tag == "gameLevelUI")
                levelUI = c;
        }
        if (GameObject.FindWithTag("levelTimer") != null)
        {
            timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
        }
        volCtrl = GameObject.FindWithTag("statistics").GetComponent<VolControl>();

    }

    void Start()
    {
        itemScorePoints = (int)(itemScorePoints * MovementForward.difficultyMultiplier);
    }

    public void HitCollectable()
    {
        levelUI.GetComponent<FloatPointUI>().GeneratePoints(itemScorePoints);
        timer.AddScore(itemScorePoints);
        timer.ContinueBonusStreak(true);
        if (thisIsSpecial)
        {
            timer.GotSpecial();
        }
        volCtrl.PlayCollectSound(this.transform.position);

        Debug.Log("collected bonus # " + bonusItemNumber);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
