using UnityEngine;
using System.Collections;

public class difficultyButton : MonoBehaviour {

    UnityEngine.UI.Text difficultyText;
	
    void Awake()
    {
        difficultyText = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }
	void Start ()
    {
        if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.normal)
            difficultyText.text = "Difficulty:\n" + "Normal";
        else if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.beginner)
            difficultyText.text = "Difficulty:\n" + "Beginner";
	}
	

    public void ChangeDifficulty()
    {
        if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.normal)
        {
            Difficulty.currentDifficulty = Difficulty.DIFFICULTY.beginner;
            difficultyText.text = "Difficulty:\n" + "Beginner";
        }
        else if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.beginner)
        {
            Difficulty.currentDifficulty = Difficulty.DIFFICULTY.normal;
            difficultyText.text = "Difficulty:\n" + "Normal";
        }
    }
}
