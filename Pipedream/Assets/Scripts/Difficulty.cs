using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour
{
    public enum DIFFICULTY { normal, beginner }
    public static DIFFICULTY currentDifficulty = DIFFICULTY.normal;

    private UnityEngine.UI.Text difficultyText;

	void Awake ()
    {
        //currentDifficulty = DIFFICULTY.normal;
        difficultyText = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
	}

	public void ChangeDifficulty ()
    {
        if (currentDifficulty == DIFFICULTY.normal)
        {
            currentDifficulty = DIFFICULTY.beginner;
            difficultyText.text = "Difficulty:\n" + "Beginner";
        }
        else if (currentDifficulty == DIFFICULTY.beginner)
        {
            currentDifficulty = DIFFICULTY.normal;
            difficultyText.text = "Difficulty:\n" + "Normal";
        }
	}
}
