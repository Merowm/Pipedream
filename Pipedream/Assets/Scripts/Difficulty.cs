using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour
{
    public enum DIFFICULTY { beginner, normal }
    public static DIFFICULTY currentDifficulty;

    private UnityEngine.UI.Text difficultyText;

	void Awake ()
    {
        currentDifficulty = DIFFICULTY.beginner;
        difficultyText = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
	}

	public void ChangeDifficulty ()
    {
        if (currentDifficulty == DIFFICULTY.beginner)
        {
            currentDifficulty = DIFFICULTY.normal;
            difficultyText.text = "Difficulty:\n" + "Normal";
        }
        else if (currentDifficulty == DIFFICULTY.normal)
        {
            currentDifficulty = DIFFICULTY.beginner;
            difficultyText.text = "Difficulty:\n" + "Beginner";
        }
	}
}
